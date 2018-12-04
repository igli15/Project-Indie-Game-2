using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
	private Player m_player;

	private Animator m_animator;

	private Health m_health;

	private DashBehaviour m_dashBehaviour;
	
	// Use this for initialization
	void Start ()
	{
		m_animator = GetComponentInChildren<Animator>();
		m_player = GetComponent<Player>();
		m_health = GetComponent<Health>();
		m_dashBehaviour = GetComponent<DashBehaviour>();
		
		m_health.OnHealthDecreased += health => m_animator.SetTrigger("isDamaged");
		m_health.OnDeath += health => m_animator.SetTrigger("isDead"); 
		m_dashBehaviour.OnDash += behaviour => m_animator.SetTrigger("dash");
		
		CompanionController.OnCompanionThrown += (controller, companion) => m_animator.SetTrigger("attackCommand");
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_animator.SetFloat("velocity",m_player.MoveSpeed);
		m_animator.SetBool("inputIsGiven",m_player.InputIsGiven);
	}
}
