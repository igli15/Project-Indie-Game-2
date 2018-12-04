using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
	[SerializeField] 
	private Player m_player;

	private Health m_health;

	private float m_maxHealth;

	private Slider m_slider;
	
	
	// Use this for initialization
	void Start ()
	{
		m_health = m_player.GetComponent<Health>();
		m_maxHealth = m_health.MaxHealth;
		m_slider = GetComponent<Slider>();

	}
	
	// Update is called once per frame
	void Update ()
	{
		m_slider.value = m_health.GetCurrentHealth / m_maxHealth;
	}
}
