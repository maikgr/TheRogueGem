using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

	public AudioMixer audioMixer;

	public void setVolume(float volume) {
		audioMixer.SetFloat("volume", volume);
	}

	public void setVolume(bool on) {
		// inversed because of checkmark logic
		if (!on) {
			audioMixer.SetFloat ("volume", 0);
		} else {
			audioMixer.SetFloat ("volume", -80);
		}
	}
}
