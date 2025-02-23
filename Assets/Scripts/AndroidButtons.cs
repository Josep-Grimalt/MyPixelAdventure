using UnityEngine;

public class AndroidButtons : MonoBehaviour
{
    private void Awake()
    {
        #if !UNITY_ANDROID
            gameObject.SetActive(false);
        #endif
    }
}
