using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBehaviour : MonoBehaviour
{

	[SerializeField] 
	private float m_dashRange = 3;

	[SerializeField] 
	private float m_hitOffset = 0.5f;
	
	[SerializeField] 
	private float m_abilityCD = 0.5f;
	private float m_initAbilityCD;

	private float m_initDashRange;

	public Action<DashBehaviour> OnDash;

	
	// Use this for initialization
	void Start ()
	{
		m_initDashRange = m_dashRange;
		m_initAbilityCD = m_abilityCD;
	}

	private void Update()
	{
		m_abilityCD -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Space) && m_abilityCD < 0)
		{
			Dash();
			m_abilityCD = m_initAbilityCD;
		}
	}

	public void Dash()
	{
		RaycastHit hit;
		
		if (OnDash != null) OnDash(this);
		
		if (Physics.Raycast(transform.position, transform.forward * m_dashRange, out hit))
		{
			if(hit.transform != transform && hit.distance <= m_dashRange)
			m_dashRange = hit.distance - m_hitOffset;
		}

		transform.position = transform.position + transform.forward * m_dashRange;
		
		m_dashRange = m_initDashRange;
	}
}
