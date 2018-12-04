using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushEffects : CompanionEffects
{
	[SerializeField] 
	private GameObject m_rushEffects;
	
	// Use this for initialization
	void Start () 
	{
		base.Start();

		m_companion.OnThrow += companion => m_rushEffects.SetActive(true);
		//m_companion.OnDisable += companion => m_rushEffects.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
