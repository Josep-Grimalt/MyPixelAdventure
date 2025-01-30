using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetFloat("Speed", 0);
            if (isDead)
                Debug.Log("Alive");
            else
                Debug.Log("Death");
            Die();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Walk");
            animator.SetFloat("Speed", 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Idle");
            animator.SetFloat("Speed", 0);
        }
    }

    void Die()
    {
        isDead = !isDead;
        animator.SetBool("Death", isDead);
    }
}
