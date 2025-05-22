using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAreaDisplay : MonoBehaviour
{
    [Header("Raycast Settings")]
    public float raycastDistance = 50f; // Raycast 최대 거리
    public LayerMask floorLayer; // 바닥으로 인식할 레이어 (Inspector에서 설정)


    [Header("Circle Display Settings")]
    public float circleRadius = 3f; // 원의 반지름
    [Range(20, 100)]
    public int circleSegments = 50; // 원을 구성하는 세그먼트(선분) 수
    public Material circleMaterial;
    public float lineWidth = 0.1f;

    private LineRenderer lineRenderer;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        InitializeLineRenderer();
    }

    void InitializeLineRenderer()
    {
        if (circleMaterial == null)
        {
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }
        else
        {
            lineRenderer.material = circleMaterial;
        }

        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

    public float GetCircleRadius()
    {
        return circleRadius;
    }

    void Update()
    {
        PerformRaycastAndDrawCircle();
    }

    void PerformRaycastAndDrawCircle()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance, floorLayer))
        {
            DrawCircleAtPoint(hit.point); // 닿은 지점에 원 그리기
        }
    }

    void DrawCircleAtPoint(Vector3 center)
    {
        lineRenderer.positionCount = circleSegments + 1;

        float angle = 0f;
        for (int i = 0; i <= circleSegments; i++)
        {
            float x = center.x + circleRadius * Mathf.Cos(angle);
            float z = center.z + circleRadius * Mathf.Sin(angle);

            Vector3 point = new Vector3(x, center.y + 0.05f, z); // 0.05f는 바닥보다 살짝 위로 띄우는 offset

            lineRenderer.SetPosition(i, point);

            angle += (2f * Mathf.PI / circleSegments);
        }
    }
}
