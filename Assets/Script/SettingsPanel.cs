using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsPanel : MonoBehaviour {

	public Slider volumeSlider;

	public AudioMixer mixer;

	// Use this for initialization
	void OnEnable () {
		volumeSlider.value =  PlayerPrefs.GetFloat(IDs.PlayerSettings.volume, 1);
	}
	
	public void OnVolumeChange(float volume)
	{
		AudioManager.Instance.SetVolume(volumeSlider.value); 
		PlayerPrefs.SetFloat(IDs.PlayerSettings.volume, volumeSlider.value);
	}
}
