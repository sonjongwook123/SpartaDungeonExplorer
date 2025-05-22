using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    [Header("점프 설정")]
    [SerializeField] private float baseJumpForce = 25f;
    // ScriptableObject 타입으로 직접 할당 가능
    [SerializeField] private JumpFloorSO jumpFloorAsset;
    [SerializeField] private Transform lastPoint;

    [Header("움직임 설정")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float moveDistance = 5f;
    // ScriptableObject 타입으로 직접 할당 가능
    [SerializeField] private MoveFloorSO moveFloorAsset;

    private void Awake()
    {
        if (jumpFloorAsset == null)
        {
        }
        if (moveFloorAsset == null)
        {
        }
        else
        {
            moveFloorAsset.Initialize(transform.position, moveSpeed, moveDistance);
        }
    }

    private void FixedUpdate()
    {
        if (moveFloorAsset != null)
        {
            moveFloorAsset.Move(transform);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                if (moveFloorAsset != null)
                {
                    other.gameObject.transform.SetParent(this.transform);
                }
                if (jumpFloorAsset != null)
                {
                    if (lastPoint != null)
                    {
                        jumpFloorAsset.ApplyJump(transform.position, lastPoint.position, 100f, 5f, other.transform);
                    }
                    else
                    {
                        jumpFloorAsset.ApplyJump(playerRigidbody, baseJumpForce);
                    }
                }
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.transform.parent == this.transform)
            {
                other.gameObject.transform.SetParent(null);
            }
        }
    }

}
