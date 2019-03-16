using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SniperCompanion : Companion
{

	[Header("Sniper Companion Values")]
	[SerializeField] 
	private float m_playerSlowAmount = 2;

	[SerializeField] 
	[Range(0,100)]
	private float m_piercingDamage = 50;

	[SerializeField] 
	[Range(0,100)]
	private float m_fullDamage = 100;
	
	private GameObject m_targetEnemy;


	private Collider m_collider;

	public Action<SniperCompanion> OnTargetHit;
	public Action<SniperCompanion> OnPiercingHit;
	
	
	private void Awake()
	{
		base.Awake();
        m_collider = GetComponent<Collider>();
	}
	
	// Use this for initialization
	void Start ()
	{

	}
	
	void Update () 
	{
		base.Update();
	}
	
	public override void Activate(GameObject other = null)
	{
		if (OnActivate != null) OnActivate(this);
	}

	public override void CheckIfOutOfRange()
	{
		base.CheckIfOutOfRange();
	}

	public override void Reset()
	{
		m_collider.enabled = false;
		m_targetEnemy = null;

		base.Reset();
	}

	public override void Throw(Vector3 dir)
	{
		m_collider.enabled = true;
		
		base.Throw(dir.normalized);
		RaycastHit[] hits;
		
		hits = Physics.RaycastAll(transform.position, dir.normalized,m_throwRange).OrderBy(d=>d.distance).ToArray();

		for (int i = 0; i < hits.Length; i++)
		{	
			if (hits[i].transform.CompareTag("Obstacle"))
			{
				break;
			}
			if (hits[i].transform.CompareTag("Enemy"))
			{
                AudioManagerScript.instance.PlaySound("companionHit");
				m_targetEnemy = hits[i].transform.gameObject;
			}
		}
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			if (m_targetEnemy != null)
			{
				if (other.transform == m_targetEnemy.transform)
				{
					if(OnTargetHit != null)OnTargetHit(this);
					other.GetComponent<Health>().InflictDamage(m_fullDamage);
					m_manager.DisableCompanion(this);
				}
				else
				{
					if(OnPiercingHit != null) OnPiercingHit(this);
					other.GetComponent<Health>().InflictDamage(m_piercingDamage);
				}
			}
		}
		else if (other.transform.CompareTag("Obstacle") && m_isThrown)
		{
			m_manager.DisableCompanion(this);
		}
		
	}
}
