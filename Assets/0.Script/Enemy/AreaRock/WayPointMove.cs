using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    public enum StoneState
    {
        Idle,
        MovingToPoint,
        WaitingAtPoint
    }

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float waitTimeAtPoint = 1f;
    public float arrivalThreshold = 0.1f;
    public Transform[] patrolPoints;

    [Header("Current State")]
    public StoneState currentState = StoneState.Idle;

    private int currentPointIndex = 0;
    private bool movingForward = true;


    void Start()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            enabled = false; // 스크립트 비활성화
            return;
        }

        // 초기 상태 설정 및 FSM 시작
        TransitionToState(StoneState.Idle);
    }

    void Update()
    {
        switch (currentState)
        {
            case StoneState.Idle:
                break;

            case StoneState.MovingToPoint:
                MoveTowardsNextPoint();
                break;

            case StoneState.WaitingAtPoint:
                break;
        }
    }

    void TransitionToState(StoneState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case StoneState.Idle:
                StartCoroutine(StartMovingAfterDelay(waitTimeAtPoint));
                break;

            case StoneState.MovingToPoint:
                break;

            case StoneState.WaitingAtPoint:
                StartCoroutine(WaitAndMove());
                break;
        }
    }

    IEnumerator StartMovingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        TransitionToState(StoneState.MovingToPoint);
    }

    // 다음 지점으로 이동하는 로직
    void MoveTowardsNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < arrivalThreshold)
        {
            TransitionToState(StoneState.WaitingAtPoint); // 도착했으니 대기 상태로 전환
        }
    }

    // 지점에 도착 후 대기하고 다음 지점으로 이동 로직
    IEnumerator WaitAndMove()
    {
        yield return new WaitForSeconds(waitTimeAtPoint);

        if (movingForward)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = patrolPoints.Length - 2; // 마지막 지점에서 역방향으로 전환
                movingForward = false;
            }
        }
        else
        {
            currentPointIndex--;
            if (currentPointIndex < 0)
            {
                currentPointIndex = 1; // 첫 지점에서 순방향으로 전환
                movingForward = true;
            }
        }

        TransitionToState(StoneState.MovingToPoint); // 다시 이동 상태로 전환
    }
}
