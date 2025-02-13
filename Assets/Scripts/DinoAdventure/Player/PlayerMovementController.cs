using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float speed, dashSpeed, dashDuration, dashCooldown, jumpSpeed;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    private PlayerMngr manager;
    private bool isDashing, canDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        manager = GetComponent<PlayerMngr>();
        isDashing = false;
        canDash = true;
    }

    private void FixedUpdate()
    {
        if (!manager.IsPlayerReady()) return;

        UpdateMovement();
    }

    public void SetDirection(CallbackContext context)
    {
        dirX = context.ReadValue<Vector2>().x;
    }

    void UpdateMovement()
    {
        if (isDashing) return;

        rb.linearVelocity = new Vector2(dirX * speed, rb.linearVelocity.y);
    }


    public void StartDash()
    {
        if (!canDash) return;

        StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        rb.linearVelocityX = dirX * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocityX = 0;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            //sndManager.GetComponent<SoundManager>().PlayFX(0)
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, jumpSpeed);
        }
    }

}
