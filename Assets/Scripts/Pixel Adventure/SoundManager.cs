using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public bool isFXEnabled = true;
    public bool isMusicEnabled = true;
	public float musicVolume;
	public float fxVolume;

	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	public AudioClip menuMusic;
	public AudioClip gameMusic;
	public AudioClip jumpFX;
	public AudioClip collectedFX;
	public AudioClip finishedFX;
	public AudioClip deadFX;

	public GameObject game;

	public static SoundManager Instance = null;

	private GameObject gameManager;
	

	void Start() {
		gameManager = GameObject.Find("GameManager");
	}

	// Play a single clip through the sound effects source.
	public void Play(AudioClip clip) {
		if (isFXEnabled) {
			EffectsSource.clip = clip;
			EffectsSource.Play();
		}
	}

	// Play a single clip through the music source.
	public void PlayMusic(AudioClip clip) {
		if (isMusicEnabled) {
			MusicSource.enabled = true;
			MusicSource.loop = true;
			MusicSource.clip = clip;
			MusicSource.Play();
		}
	}
	
	public void PlayFX(int i) {
		if (isFXEnabled) {
			EffectsSource.enabled = true;

			if (i == 0) {
				EffectsSource.clip = jumpFX;
			} else if (i == 1) {
				EffectsSource.clip = collectedFX;
			} else if (i == 2) {
				EffectsSource.clip = finishedFX;
			} else if (i == 3) {
				EffectsSource.clip = deadFX;
			}
			EffectsSource.Play();
		}
	}

	// Play a single clip through the music source.
	public void PlayMusic(int i) {
		if (isMusicEnabled) {
			MusicSource.enabled = true;
			MusicSource.loop = true;
			if (i == 0) { 
				MusicSource.clip = menuMusic;
			} else if (i == 1) {
				MusicSource.clip = gameMusic;
			}
			MusicSource.Play();
		}
	}

	public void StopMusic() {
		MusicSource.Stop();
	}

	public void SetMusicVolume(float f) {
		musicVolume = f;
		MusicSource.volume = f;
	}

	public void SetFXVolume(float f) {
		fxVolume = f;
		EffectsSource.volume = f;
	}

	public void SetEnableFX(bool b) {
		isFXEnabled = b;
		if (b) PlayFX(0);
	}

	public void SetEnableMusic(bool b) {
		isMusicEnabled = b;
		if (!b) 
			StopMusic();
		else
			PlayMusic(0);
	}
}