using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Wandering
}

public class Zombie : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState aiState;

    [Header("Area Costs")]
    public int mudAreaIndex;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private float playerDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Wandering:
                PassiveUpdate();
                break;
        }
    }

    private void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
        }
    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;

        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        Vector3 randomDirection;
        Vector3 randomPoint;

        int attempts = 0;
        int maxAttempts = 30; // 최대 시도 횟수 설정

        int walkableAreaMask = NavMesh.AllAreas & ~(1 << mudAreaIndex); // 이진변환 이용 (ai할때 이미지 이진변환후 인식률 높일때도 쓰임임)

        while (attempts < maxAttempts)
        {
            randomDirection = Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance);
            randomPoint = transform.position + randomDirection;

            if (NavMesh.SamplePosition(randomPoint, out hit, maxWanderDistance, walkableAreaMask))
            {
                return hit.position; // 유효한 위치를 찾으면 반환
            }
            attempts++;
        }
        return transform.position; // 유효한 위치를 찾지 못하면 현재 위치 반환
    }

}
