using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    float originSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    public bool isDoubleJump;
    public int jumpCount;

    public event Action<bool> OnDash;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    private Vector2 mouseDelta;

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody _rigidbody;
    public Transform[] cameras;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (GetComponent<Player>() != null)
        {
            GetComponent<Player>().OnDoubleJump += DoubleJumpUpdate;
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().OnDoubleJump += DoubleJumpUpdate;
        }
        originSpeed = moveSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    void DoubleJumpUpdate(bool value)
    {
        this.isDoubleJump = value;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (jumpCount < 2 && isDoubleJump == true)
            {
                jumpCount += 1;
                _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                if (IsGrounded() == true)
                {
                    _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                }
            }
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && IsGrounded() == true)
        {
            moveSpeed *= 2;
            OnDash?.Invoke(true);
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            moveSpeed = originSpeed;
            OnDash?.Invoke(false);
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + (transform.forward * 0.3f) + (transform.up * 0.03f), Vector3.down);
        Debug.DrawRay(ray.origin, ray.direction * 0.3f, Color.red, 0.3f);

        if (Physics.Raycast(ray, 0.1f, groundLayerMask))
        {
            jumpCount = 0;
            return true;
        }
        return false;
    }

    public void OnChangeCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (cameras[0].gameObject.activeSelf == true)
            {
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);
            }
            else
            {
                cameras[1].gameObject.SetActive(false);
                cameras[0].gameObject.SetActive(true);
            }
        }
    }
}
