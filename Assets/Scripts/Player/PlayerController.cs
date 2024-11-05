using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public float slideDuration = 0.5f;
    private const float LANE_DISTANCE = 5.0f; // ���� ���� �Ÿ�

    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Rigidbody rb;
    private bool isSliding = false;
    private int jumpCount = 0;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float customGravity = -9.81f;

    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    private ObstacleCollisionProcessor collisionProcessor;

    [SerializeField] private CapsuleCollider slideCollider;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collisionProcessor = GetComponent<ObstacleCollisionProcessor>();
        originalColliderHeight = slideCollider.height;
        originalColliderCenter = slideCollider.center;
    }

    private void Update()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z = transform.position.z;

        if (desiredLane == 0)
        {
            targetPosition.x = -LANE_DISTANCE;
        }

        else if (desiredLane == 1)
        {
            targetPosition.x = 0;
        }

        else if (desiredLane == 2)
        {
            targetPosition.x = LANE_DISTANCE;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.up * customGravity * fallMultiplier, ForceMode.Acceleration);
        }

        else
        {
            rb.AddForce(Vector3.up * customGravity, ForceMode.Acceleration);
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (desiredLane > 0)
            {
                desiredLane--; // �������� ���� �̵�
            }

            else if (desiredLane == 0)
            {
                // ���� ������ ���� �̵� �õ� �� ������ ó��
                //collisionProcessor.TriggerHitObstacle();
            }
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            if (desiredLane < 2)
            {
                desiredLane++; // ���������� ���� �̵�
            }

            else if (desiredLane == 2)
            {
                // ������ ������ ������ �̵� �õ� �� ������ ó��
                //collisionProcessor.TriggerHitObstacle();
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && (IsGrounded() || jumpCount < 2))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isSliding)
        {
            StartCoroutine(Slide());
        }
    }

    private bool IsGrounded()
    {
        bool grounded = rb.velocity.y <= 1f && Physics.CheckSphere(transform.position, groundCheckRadius, groundLayerMask);
        if (grounded)
        {
            jumpCount = 0;
        }

        return grounded;
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        slideCollider.height = originalColliderHeight * 0.5f;
        slideCollider.center = originalColliderCenter * 0.5f;

        yield return new WaitForSeconds(slideDuration);

        slideCollider.height = originalColliderHeight;
        slideCollider.center = originalColliderCenter;

        isSliding = false;
    }

    public void InitializeMovement()
    {
        rb.velocity = Vector3.zero;
        desiredLane = 1;
        isSliding = false;
        jumpCount = 0;

        slideCollider.height = originalColliderHeight;
        slideCollider.center = originalColliderCenter;
    }
}