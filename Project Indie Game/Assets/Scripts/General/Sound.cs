using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
	[Header("Sound Clips")]
	public string name;
	
	public AudioClip clip;

	[Range(0,1)]
	public float volume;

	[Range(0.1f,3)]
	public float pitch;

	[Range(0f,1)]
	public float spatialBlend;
	
	[Range(0f,5)]
	public float doplerEffect;

	public bool loop;

	public AudioRolloffMode RollOffMode;

	[HideInInspector] 
	public AudioSource audioSource;

}
