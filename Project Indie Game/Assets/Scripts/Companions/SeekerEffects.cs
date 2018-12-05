using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekerEffects : CompanionEffects
{

	[SerializeField]
	private TrailRenderer m_trailRenderer;

	// Use this for initialization
	void Start () 
	{
		base.Start();
		m_companion = GetComponent<ACompanion>();
		m_companion.OnThrow += EnableTrail;
		m_companion.OnDisable += companion => m_trailRenderer.gameObject.transform.SetParent(null);
	}

	void ResetTrailTime()
	{
		m_trailRenderer.time = 2;
	}

	void EnableTrail(ACompanion companion)
	{
		m_trailRenderer.enabled = true;
		m_trailRenderer.Clear();
		m_trailRenderer.gameObject.transform.SetParent(transform);
		m_trailRenderer.transform.position = transform.position;
		m_trailRenderer.time = -1;
		Invoke("ResetTrailTime",0.1f);
		m_trailRenderer.Clear();
		
	}


	private void OnEnable()
	{
		m_trailRenderer.transform.position = transform.position;
		m_trailRenderer.Clear();
	}
}
