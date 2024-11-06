using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 50f;
    public float slideDuration = 0.5f;
    private const float LANE_DISTANCE = 5.0f; // 레인 간의 거리

    private int desiredLane = 1; // 0 = Left, 1 = Middle, 2 = Right  <- 이런 건 int가 아니라 enum으로 만드는 게 좋습니다...
    private Rigidbody rb;
    private bool isSliding = false;
    private int jumpCount = 0;
    private int lastLane;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float customGravity = -9.81f;

    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    [SerializeField] private CapsuleCollider slideCollider;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private LayerMask structureLayerMask;
    [SerializeField] private float groundCheckRadius;

    [SerializeField] private Collider sideCollisionCheckCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalColliderHeight = slideCollider.height;
        originalColliderCenter = slideCollider.center;
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused) return;

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
        if (GameManager.Instance.isPaused) return;

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
        if (GameManager.Instance.isPaused || context.phase != InputActionPhase.Performed) return;
        {
            if (desiredLane > 0)
            {
                lastLane = desiredLane--; // 왼쪽으로 레인 이동

                AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_47, transform.position, 0.5f);
                AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Yat, transform.position, 0.5f);
            }

            else if (desiredLane == 0)
            {
                GameManager.Instance.player.stats.TakeDamage(10);
            }
        }
    }

    public void OnMoveRight(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isPaused || context.phase != InputActionPhase.Performed) return;
        {
            if (desiredLane < 2)
            {
                lastLane = desiredLane++; // 오른쪽으로 레인 이동

                AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_47, transform.position, 0.5f);
                AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Yat, transform.position, 0.5f);
            }

            else if (desiredLane == 2)
            {
                GameManager.Instance.player.stats.TakeDamage(10);
            }
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isPaused || context.phase != InputActionPhase.Started) return;

        if (IsGrounded() || jumpCount < 2)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_46, transform.position, 0.5f);
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Yat, transform.position, 0.5f);
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.isPaused || !context.performed || !IsGrounded() || isSliding) return;
        {
            StartCoroutine(Slide());
        }

        AudioManager.Instance.PlaySoundFXClip(AudioClipName.Sfx_47, transform.position, 0.5f);
        AudioManager.Instance.PlaySoundFXClip(AudioClipName.UnityChan_Yat, transform.position, 0.5f);
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

    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameManager.Instance.TogglePause();
        }
    }

    public void OnRestartGame(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void GoToTitleScene(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            GameManager.Instance.GoToTitleScene();
        }
    }

    public void SideCollision()
    {
        desiredLane = lastLane;
    }
}