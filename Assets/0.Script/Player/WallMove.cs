using UnityEngine;

public class WallMove : MonoBehaviour
{
    public Rigidbody rb;
    public float wallCheckDistance = 0.5f;
    public float wallStickForce = 150f;
    public float slideGravityScale = 0.2f;

    private bool isWallSliding = false;
    private Vector3 currentWallNormal; // 벽의 법선 벡터

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckForWall();

        if (isWallSliding)
        {
            ApplyWallSlidePhysics();
        }
        else
        {
            if (!rb.useGravity)
            {
                rb.useGravity = true;
            }
        }
    }

    void CheckForWall()
    {
        RaycastHit hit;
        bool wallHit = Physics.Raycast(transform.position, transform.forward, out hit, wallCheckDistance) ||
        Physics.Raycast(transform.position, -transform.right, out hit, wallCheckDistance) ||
        Physics.Raycast(transform.position, transform.right, out hit, wallCheckDistance);

        bool isCurrentlyGrounded = Physics.Raycast(transform.position, Vector3.down, 0.2f); // 플레이어 발 밑 레이캐스트


        if (wallHit && !isCurrentlyGrounded)
        {
            // 벽의 법선 벡터를 저장
            currentWallNormal = hit.normal;

            if (!isWallSliding)
            {
                StartWallSlide();
            }
        }
        else // 벽에서 떨어졌거나, 바닥에 닿았을 때
        {
            if (isWallSliding)
            {
                StopWallSlide();
            }
        }
    }

    void StartWallSlide()
    {
        isWallSliding = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

    void StopWallSlide()
    {
        isWallSliding = false;
        rb.useGravity = true;
    }

    void ApplyWallSlidePhysics()
    {
        rb.AddForce(currentWallNormal * wallStickForce * Time.fixedDeltaTime, ForceMode.Force);
        Vector3 newVelocity = rb.velocity;

        newVelocity.x = Mathf.Lerp(newVelocity.x, 0, Time.fixedDeltaTime * 10f);
        newVelocity.z = Mathf.Lerp(newVelocity.z, 0, Time.fixedDeltaTime * 10f);

        newVelocity.y -= Physics.gravity.y * slideGravityScale * Time.fixedDeltaTime;
        rb.velocity = newVelocity;
    }
}
