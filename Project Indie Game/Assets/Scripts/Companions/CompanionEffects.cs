using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionEffects : MonoBehaviour
{
	
	[SerializeField] 
	private ParticleSystem m_chargeParticleSystem; 

	// Use this for initialization
	protected void Start ()
	{
		CompanionController.OnMouseCharging += EnableChargeEffect;
		CompanionController.OnMouseRelease += DisableChargeEffect;
		
	}

	protected void EnableChargeEffect(CompanionController controller,ACompanion companion)
	{
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

	protected void DisableChargeEffect(CompanionController controller, ACompanion companion)
	{
		m_chargeParticleSystem.transform.parent.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		CompanionController.OnMouseCharging -= EnableChargeEffect;
		CompanionController.OnMouseRelease -= DisableChargeEffect;
	}
}
