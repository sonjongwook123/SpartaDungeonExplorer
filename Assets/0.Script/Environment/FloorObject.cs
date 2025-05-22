using UnityEngine;

public interface IJumpFloor
{
    void ApplyJump(Rigidbody playerRigidbody, float jumpForce);
}

public interface IMoveFloor
{
    void Initialize(Vector3 startPosition, float moveSpeed, float moveDistance);
    void Move(Transform platformTransform);
}


public abstract class JumpFloorSO : ScriptableObject, IJumpFloor
{
    public abstract void ApplyJump(Rigidbody playerRigidbody, float jumpForce);
    public abstract void ApplyJump(Vector3 startPoint, Vector3 endPoint, float height, float t, Transform player);
}

// IMoveStrategy를 구현하는 ScriptableObject 기반 추상 클래스
public abstract class MoveFloorSO : ScriptableObject, IMoveFloor
{
    // 움직임 전략에 필요한 공통 필드를 여기에 추가할 수 있습니다.
    [SerializeField] protected float moveSpeed = 2f;
    [SerializeField] protected float moveDistance = 5f;

    protected Vector3 startPosition; // 초기화 시 설정될 시작 위치

    public abstract void Initialize(Vector3 startPosition, float moveSpeed, float moveDistance);
    public abstract void Move(Transform platformTransform);
}

[CreateAssetMenu(fileName = "JumpFloorNormal", menuName = "FLoor/Jump/Normal")]
public class FloorObjectJumpNormal : JumpFloorSO
{
    public override void ApplyJump(Rigidbody playerRigidbody, float jumpForce)
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
    }

    public override void ApplyJump(Vector3 startPoint, Vector3 endPoint, float height, float t, Transform player) { }
}

[CreateAssetMenu(fileName = "JumpFloorParabolic", menuName = "FLoor/Jump/Parabolic")]
public class FloorObjectJumpParabolic : JumpFloorSO
{
    public override void ApplyJump(Rigidbody playerRigidbody, float jumpForce) { }

    public override void ApplyJump(Vector3 startPoint, Vector3 endPoint, float height, float t, Transform player)
    {
        player.GetComponent<PlayerEffected>().TakeParabolicJump(startPoint, endPoint, height, t);
    }
}


[CreateAssetMenu(fileName = "MoveFloor", menuName = "FLoor/Move/Normal")]
public class FloorObjectMoveNormal : MoveFloorSO
{
    private bool _movingRight;

    public override void Initialize(Vector3 startPosition, float moveSpeed, float moveDistance)
    {
        this.startPosition = startPosition;
        this.moveSpeed = moveSpeed;
        this.moveDistance = moveDistance;
    }

    public override void Move(Transform platformTransform)
    {
        Vector3 currentTarget;
        if (_movingRight)
        {
            currentTarget = startPosition + Vector3.right * moveDistance;
        }
        else
        {
            currentTarget = startPosition + Vector3.left * moveDistance;
        }

        platformTransform.position = Vector3.MoveTowards(platformTransform.position, currentTarget, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(platformTransform.position, currentTarget) < 0.05f)
        {
            _movingRight = !_movingRight;
        }
    }
}
