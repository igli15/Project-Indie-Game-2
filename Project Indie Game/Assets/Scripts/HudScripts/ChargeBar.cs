using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour
{
	private Slider m_slider;

	private float m_timeCharging;
	
	// Use this for initialization
	void Start ()
	{
		m_slider = GetComponent<Slider>();
		m_slider.value = 0;
		CompanionController.OnMouseCharging += ChargeSlider;
		CompanionController.OnMouseRelease += RestoreSlider;
	}

	public void ChargeSlider(CompanionController controller,ACompanion companion)
	{
		/*if (companion is BlackHoleCompanion)
		{
			BlackHoleCompanion blackHoleCompanion = (companion as BlackHoleCompanion); 
			m_slider.value = (blackHoleCompanion.ThrowRange - blackHoleCompanion.MinDistance) /
			                 (blackHoleCompanion.MaxDistance - blackHoleCompanion.MinDistance);

		}*/
			m_timeCharging = controller.TimeCharging;

			if (m_timeCharging > companion.ChargeTime)
			{
				m_timeCharging = companion.ChargeTime;
			}

			if (companion.ChargeTime <= 0.1f) m_slider.value = 1;
			else
			{
				m_slider.value = m_timeCharging / companion.ChargeTime;
			}
		
	}

	public void RestoreSlider(CompanionController controller,ACompanion companion)
	{
		m_timeCharging = 0;
		m_slider.value = 0;
	}
}
