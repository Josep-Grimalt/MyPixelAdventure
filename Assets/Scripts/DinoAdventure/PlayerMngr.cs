using UnityEngine;

public class PlayerMngr : MonoBehaviour
{
    private GameObject gameManager;
    //private GameObject sndManager;
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpSpeed = 7f;
    [SerializeField] private float dashSpeed = 70f, dashDuration = 0.2f, dashCooldown = 1;
    private float dirX;
    private bool canDash;

    private int lifes;
    private bool isPlayerReady;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        //sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        Invoke("InitPlayer", 0.75f);
    }

    void InitPlayer()
    {
        lifes = 3;
        isDead = false;
        isPlayerReady = true;
        canDash = true;
    }

    void Update()
    {
        if (!isPlayerReady) return;

        //GetAxis
        dirX = Input.GetAxisRaw("Horizontal");

        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            //sndManager.GetComponent<SoundManager>().PlayFX(0);
            GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, jumpSpeed);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        //HACK!
        if (Input.GetKeyDown(KeyCode.P))
        {
            rb.bodyType = RigidbodyType2D.Static;
            Invoke("CompleteLevel", 0.25f);
        }
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        rb.linearVelocity = new Vector2(dirX * speed, rb.linearVelocity.y);
    }

    void UpdateAnimator()
    {
        if (dirX > 0f)
        {
            anim.SetFloat("Speed", 1);
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (dirX < 0f)
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

    IEnumerator Dash()
    {
        canDash = false;

        rb.linearVelocityX = dirX * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocityX = 0;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    bool IsGrounded()
    {
        //return (rb.velocity.y == 0f)?true:false;
        /*
        if (rb.velocity.y == 0f) 
        {
            return true;
        } else {
            return false;
        }
        */
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void KillPlayer()
    {
        isDead = true;
        isPlayerReady = false;
        lifes -= 1;
        //sndManager.GetComponent<SoundManager>().PlayFX(3);
        anim.SetTrigger("Death");
        anim.SetBool("Dead", true);
        if (rb.linearVelocityY < 0.1f)
            rb.bodyType = RigidbodyType2D.Kinematic;
        dirX = 0;

        if (lifes > 0)
        {
            Invoke("RestartLevel", 2f);
        }
        else
        {
            //TEXT GAMEOVER
            Invoke("GameOver", 2f);
        }
    }

    void CompleteLevel()
    {
        gameManager.GetComponent<GameManager>().CompleteLevel();
    }

    void RestartLevel()
    {
        if (!gameManager) return;

        gameManager.GetComponent<GameManager>().RestartLevel();
    }

    void GameOver()
    {
        gameManager.GetComponent<GameManager>().GameOver();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Trap") || c.gameObject.CompareTag("Enemy"))
        {
            KillPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Finish"))
        {
            isPlayerReady = false;
            //sndManager.GetComponent<SoundManager>().PlayFX(2);
            rb.bodyType = RigidbodyType2D.Static;
            Invoke("CompleteLevel", 2f);
        }

        if (c.gameObject.CompareTag("Death"))
        {
            KillPlayer();
        }
    }
}