using System.Collections;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMngr manager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<PlayerMngr>();
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Trap") || c.gameObject.CompareTag("Enemy"))
        {
            manager.KillPlayer();
        }

        if (c.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(c.transform);
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Finish"))
        {
            manager.LevelCompleted();
            rb.bodyType = RigidbodyType2D.Static;
            StartCoroutine(CompleteLevel(2f));
        }

        if (c.gameObject.CompareTag("Death"))
        {
            manager.KillPlayer();
        }

        if (c.gameObject.CompareTag("Collectible"))
        {
            manager.Collect();
            Destroy(c.gameObject);
        }
        if (c.gameObject.CompareTag("Wall"))
        {
            if (rb.linearVelocityX > 15)
            {
                Destroy(c.gameObject);
            }
        }
    }

    private IEnumerator CompleteLevel(float delay)
    {
        yield return new WaitForSeconds(delay);

        manager.CompleteLevel();
    }
}
