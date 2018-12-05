using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SniperCompanion))]
public class SniperEffects : CompanionEffects
{
	[SerializeField]
	private GameObject impactEffect;

	private SniperCompanion m_sniperCompanion;

	// Use this for initialization
	void Start () 
	{
		base.Start();
		m_sniperCompanion = GetComponent<SniperCompanion>();

		m_sniperCompanion.OnTargetHit += companion => EnableImpactEffect();
		
	}


	public void EnableImpactEffect()
	{
		GameObject effect = Instantiate(impactEffect, transform.position,Quaternion.identity);
		Destroy(effect,0.5f);
	}
	
	
}
