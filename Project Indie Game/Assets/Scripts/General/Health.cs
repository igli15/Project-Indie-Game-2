﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{			
	public  Action<Health> OnDeath;
	public  Action<Health> OnHealthIncreased;
	public  Action<Health> OnHealthDecreased;

	[SerializeField] 
	private float m_health = 100;

	[SerializeField] 
	private float m_maxHealth = 100;

	private float m_initialHealth;
	
	[SerializeField] 
	private bool m_shouldBeDestroyed;

    private bool m_canTakeDamage = true;
	
	void Start ()
	{
		m_initialHealth = m_health;
	}

	public void InflictDamage(float damageAmount)
	{
        if (m_canTakeDamage)
        {
            if (OnHealthDecreased != null) OnHealthDecreased(this);
            m_health -= damageAmount;

        }
       // else Debug.Log("DAMAGE IGNORE");

    }
	
	public void HealUp(float healAmount)
	{
		if(OnHealthIncreased != null) OnHealthIncreased(this);
		m_health += healAmount;
	}

	public void ResetHealth()
	{
		m_health = m_initialHealth;
	}



    public float HP { get { return m_health; } }
	
	void Update () 
	{
		if (m_health <= 0)
		{
            m_canTakeDamage = false;
            if (OnDeath != null)	OnDeath(this);

			if (m_shouldBeDestroyed)
			{
				Destroy(gameObject);
			}
		
		}

		if (m_health > m_maxHealth)
		{
			m_health = m_maxHealth;
		}
	}

    public bool CanTakeDamage { get { return m_canTakeDamage; }set { m_canTakeDamage = value; } }

	public float MaxHealth
	{
		get { return m_maxHealth; }
	}

	public float GetCurrentHealth
	{
		get { return m_health; }
	}
}
