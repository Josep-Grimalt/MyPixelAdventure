using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMngr : MonoBehaviour
{
    [SerializeField] private Text itemsText;

    private GameObject gameManager;
    private GameObject sndManager;
    private int lifes, itemsCollected;
    private bool isPlayerReady;
    private Rigidbody2D rb;
    private PlayerAnimatorController anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<PlayerAnimatorController>();

        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        Invoke(nameof(InitPlayer), 0.75f);
    }

    void InitPlayer()
    {
        lifes = 3;
        itemsCollected = 0;
        isPlayerReady = true;
    }

    void Update()
    {
        //HACK!
        if (Input.GetKeyDown(KeyCode.P))
        {
            rb.bodyType = RigidbodyType2D.Static;
            Invoke(nameof(CompleteLevel), 0.25f);
        }
    }

    public void Collect()
    {
        itemsCollected++;
        itemsText.text = "ITEMS: " + itemsCollected;
    }

    public void KillPlayer()
    {
        isPlayerReady = false;
        lifes--;
        sndManager.GetComponent<SoundManager>().PlayFX(3);
        GetComponent<Collider2D>().enabled = false;

        anim.Die();

        if (lifes > 0)
        {
            Invoke(nameof(RestartLevel), 2f);
        }
        else
        {
            //TEXT GAMEOVER
            Invoke(nameof(GameOver), 2f);
        }
    }

    public bool IsPlayerReady()
    {
        return isPlayerReady;
    }

    public void LevelCompleted()
    {
        isPlayerReady = false;
        sndManager.GetComponent<SoundManager>().PlayFX(2);
    }

    public void CompleteLevel()
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
}