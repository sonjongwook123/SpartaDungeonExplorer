using System.Collections;
using UnityEngine;

public class PlayerEffected : MonoBehaviour
{
    Rigidbody rb;
    float maxTime;
    Vector3 startPoint;
    Vector3 endPoint;
    float height;

    // 현재 포물선 점프 중인지 나타내는 플래그
    private bool isParabolicJumping = false;

    void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void TakeParabolicJump(Vector3 startPoint, Vector3 endPoint, float height, float t)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.height = height;
        this.maxTime = t;
        StartCoroutine(ApplyJumpCoroutine());
    }

    public IEnumerator ApplyJumpCoroutine()
    {
        if (isParabolicJumping) yield break; // 이미 점프 중이면 코루틴 종료

        isParabolicJumping = true;
        rb.velocity = Vector3.zero; // 현재 속도 초기화
        rb.angularVelocity = Vector3.zero; // 회전 속도 초기화

        float startTime = Time.time;
        float t = 0f;

        rb.useGravity = false;
        while (t < 1.0f)
        {
            float elapsed = Time.time - startTime;
            t = elapsed / maxTime;

            Vector3 horizontalPos = Vector3.Lerp(startPoint, endPoint, t);
            float currentY = 4 * height * t * (1 - t);
            Vector3 currentPosition = new Vector3(horizontalPos.x, startPoint.y + currentY, horizontalPos.z);
            rb.position = currentPosition;

            yield return null;
        }

        rb.useGravity = true;
        isParabolicJumping = false;
    }

}