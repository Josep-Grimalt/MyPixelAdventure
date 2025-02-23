using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
	// Initialize the singleton instance.
	private void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundManager");
		if (objs.Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			DontDestroyOnLoad(gameObject);
		}

		objs = GameObject.FindGameObjectsWithTag("GameManager");
		if (objs.Length > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			//Set GameManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			DontDestroyOnLoad(gameObject);
		}
	}
}
