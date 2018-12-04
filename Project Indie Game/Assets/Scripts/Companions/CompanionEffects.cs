using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionEffects : MonoBehaviour
{
	
	[SerializeField] 
	private ParticleSystem m_chargeParticleSystem;

	protected ACompanion m_companion;
	
	// Use this for initialization
	protected void Start ()
	{
		m_companion = GetComponent<ACompanion>();
		
		m_companion.OnCharging += EnableChargeEffect;
		m_companion.OnChargeInterrupted += DisableChargeEffect;
		
	}

	protected void EnableChargeEffect(ACompanion companion,float time)
	{
		Debug.Log("GEEGE");
		m_chargeParticleSystem.transform.parent.gameObject.SetActive(true);
		
		var main = m_chargeParticleSystem.main;

		float companionChargeTime = companion.ChargeTime;

		float simSpeed = 0;
		if (companionChargeTime < 1)
		{
			simSpeed = m_chargeParticleSystem.main.startLifetimeMultiplier/ companionChargeTime;
		}

		else
		{
			simSpeed = companionChargeTime * m_chargeParticleSystem.main.startLifetimeMultiplier;
		}
		

		main.simulationSpeed = simSpeed;
	}

	protected void DisableChargeEffect(ACompanion companion,float time)
	{
		m_chargeParticleSystem.transform.parent.gameObject.SetActive(false);
	}


}
