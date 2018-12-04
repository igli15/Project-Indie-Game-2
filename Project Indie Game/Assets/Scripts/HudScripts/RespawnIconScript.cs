using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnIconScript : MonoBehaviour
{

	private ACompanion m_companion;

	private Image m_image;
	
	
	// Use this for initialization
	void Start ()
	{
		m_image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(m_companion!= null && m_companion.RespawnTime > 0)
		m_image.fillAmount = Mathf.Abs(1 - (m_companion.RespawnCounter  / m_companion.RespawnTime));
	}


	public void AssignCompanion(ACompanion companion)
	{
		m_companion = companion;
	}
}
