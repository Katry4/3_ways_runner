using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

	public AudioMixer mixer;
	public AudioSource soundSource;
	public AudioClip coinSound, powerUpSound, gameOverSound;



	public int mixerMinVolume = -80;
	public int mixerMaxVolume = 0;


	//SingleTone is not needed because Instance is always not null
	public static AudioManager Instance;


	// Use this for initialization
	void OnEnable()
	{
		DontDestroyOnLoad(gameObject);
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
	}

	public void Start()
	{
		SetVolume( PlayerPrefs.GetFloat(IDs.PlayerSettings.volume, 1));
	}

	public void SetVolume(float volume)
	{
		mixer.SetFloat(IDs.PlayerSettings.volume, mixerMinVolume + (mixerMaxVolume - mixerMinVolume) * volume);
	}

	public void PlayCoinSound()
	{
		PlaySound(coinSound);
	}

	public void PlayPowerUpSound()
	{
		PlaySound(powerUpSound);
	}

	public void PlayGameOverSound()
	{
		PlaySound(gameOverSound);
	}

	public void PlayButtonPressSound()
	{
		//PlaySound(Random.value > 0.5 ? coinSound : powerUpSound);
		PlayCoinSound();
	}

	private void PlaySound(AudioClip clip)
	{
		if (soundSource.isPlaying)
		{
			soundSource.Stop();
		}
		soundSource.clip = clip;
		soundSource.Play();
	}
}
