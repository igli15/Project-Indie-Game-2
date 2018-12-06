using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashBehaviour : MonoBehaviour
{

	[SerializeField] 
	private float m_dashRange = 3;

	[SerializeField] 
	private float m_hitOffset = 0.5f;

	[SerializeField] 
	[Range(0, 0.5f)]
	private float m_dashTime = 0.3f;
	
	[SerializeField] 
	private float m_abilityCD = 0.5f;
	private float m_initAbilityCD;

	private float m_initDashRange;

	private bool m_dashed = false;

	private DisablePlayer m_disablePlayer;

	public Action<DashBehaviour> OnDash;

	public UnityEvent OnDashStart;
	public UnityEvent OnDashFinished;

	
	// Use this for initialization
	void Start ()
	{
		m_disablePlayer = GetComponent<DisablePlayer>();
		m_initDashRange = m_dashRange;
		m_initAbilityCD = m_abilityCD;
		m_abilityCD = 0;
	}

	private void Update()
	{
		m_abilityCD -= Time.deltaTime;
		if (!m_dashed && Input.GetKeyDown(KeyCode.Space) && m_abilityCD < 0)
		{

			StartCoroutine("Dash");
			m_abilityCD = m_initAbilityCD;
		}
	}

	IEnumerator Dash()
	{
		RaycastHit hit;
		if (OnDash != null) OnDash(this);
		
		if (Physics.Raycast(transform.position, transform.forward * m_dashRange, out hit))
		{
			if(hit.transform != transform && hit.transform.CompareTag("projectileObstacle")&&  hit.distance <= m_dashRange)
			m_dashRange = hit.distance - m_hitOffset;
		}

		Vector3 newPos = transform.position + transform.forward * m_dashRange;
		OnDashStart.Invoke();
		m_disablePlayer.Disableplayer();

		yield return new WaitForSeconds(m_dashTime);
		
		m_disablePlayer.ActivatePlayer();
		OnDashFinished.Invoke();
		
		transform.position = newPos;
		m_dashed = false;
		
		m_dashRange = m_initDashRange;
	}
}
