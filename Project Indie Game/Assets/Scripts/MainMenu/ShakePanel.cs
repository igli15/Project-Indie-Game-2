using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShakePanel : MonoBehaviour
{
	[SerializeField] 
	private Vector3 m_shakeStrength;

	[SerializeField] 
	private float m_shakeCompletionTime;
	
	[SerializeField] 
	private int m_vibrato;

	[SerializeField] 
	private float m_randomness;
	
	[SerializeField] 
	private bool m_fadeout = false;
	
	// Use this for initialization
	void Start () 
	{
		InvokeRepeating("Shake",0,m_shakeCompletionTime + 0.1f);
	}

	void Shake()
	{
		transform.DOShakePosition(m_shakeCompletionTime, m_shakeStrength, m_vibrato, m_randomness, false, m_fadeout);
	}
}
