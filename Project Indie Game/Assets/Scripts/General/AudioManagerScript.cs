using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{

	public Sound[] sounds;

	public static AudioManagerScript instance;
	
	// Use this for initialization
	private void Awake() 
	{
		//DontDestroyOnLoad(this);

		
		//NOTE
		#region SINGELTON          
		
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
		
		#endregion                                   
		
		foreach (Sound sound in sounds)
		{
			sound.audioSource = gameObject.AddComponent<AudioSource>();
			sound.audioSource.clip = sound.clip;
			sound.audioSource.volume = sound.volume;
			sound.audioSource.pitch = sound.pitch;
			sound.audioSource.dopplerLevel = sound.doplerEffect;
			sound.audioSource.spatialBlend = sound.spatialBlend;
			sound.audioSource.loop = sound.loop;
			sound.audioSource.rolloffMode = sound.RollOffMode;
		}

        PlaySound("background_1");
        PlaySound("background_2");
    }


	public void PlaySound(string soundName )
	{
		Sound soundClip = Array.Find(sounds, sound => sound.name == soundName);

		if (soundClip == null)
		{
			Debug.LogWarning("hey you made a typo, check sound with name:  " + soundName);
			return ;
		}
		soundClip.audioSource.Play();
	}
	
	public void StopSound(string soundName )
	{
		Sound soundClip = Array.Find(sounds, sound => sound.name == soundName);

		if (soundClip == null)
		{
			Debug.LogWarning("hey you made a typo, check sound with name:  " + soundName);
			return ;
		}
		soundClip.audioSource.Stop();
	}

	public void PauseAllSounds(string except = null)
	{
		foreach (Sound sound in sounds)
		{
			if (sound != null &&  sound.name != except)
			{
				sound.audioSource.Pause();
			}
		}
	}
	
	public void ResumeAllSounds()
	{
		foreach (Sound sound in sounds)
		{
			if (sound != null && sound.audioSource.isPlaying == false)
			{
				sound.audioSource.UnPause();
			}
		}
	}

	public Sound GetSoundByName(string soundName)
	{
		if (soundName != null)
		{
			return Array.Find(sounds, sound => sound.name == soundName);
		}
		else
		{
			return null;
		}
	}
}
