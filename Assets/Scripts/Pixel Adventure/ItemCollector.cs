using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int items = 0;
    [SerializeField] private Text itemsText;
    private GameObject sndManager;

    void Start() {
        itemsText.text = "Items: " + items;
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("Collectible")) 
        {
            items++;
            itemsText.text = "Items: " + items;
            col.gameObject.GetComponent<Animator>().SetTrigger("collected");
            sndManager.GetComponent<SoundManager>().PlayFX(1);
            Destroy(col.gameObject);
        }
    }


}
