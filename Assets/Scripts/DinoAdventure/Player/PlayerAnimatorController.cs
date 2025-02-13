using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        if (rb.linearVelocity.x > 0f)
        {
            anim.SetFloat("Speed", 1);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (rb.linearVelocity.x < 0f)
        {
            anim.SetFloat("Speed", 1);
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        if (rb.linearVelocity.y > .1f)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Fall", false);
        }
        else if (rb.linearVelocity.y < -.1f)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", true);
        }
        else
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }
    }

    public void Die()
    {
        anim.SetTrigger("Death");
        anim.SetBool("Dead", true);
    }
}
