using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public abstract class ACompanion : MonoBehaviour

{
	 [Header("General Companion Values")]

	public static List<ACompanion> AllCompanions = new List<ACompanion>();

	[SerializeField]
	protected Sprite m_iconSprite;

	[SerializeField] 
	protected float m_respawnTime = 3;
	
	[SerializeField]
	protected float m_chargeTime = 0;
	
	[SerializeField]
	protected float m_throwRange = 10;

	protected bool m_isCharged = false;
	
	protected int m_index;
	
	protected bool m_isThrown = false;

	protected CompanionSteering m_steering;

	protected bool m_isInParty = false;
	
	protected float m_respawnCounter = 0;
	
	public Action<ACompanion> OnSpawn;
	public Action<ACompanion> OnDisable;
	public Action<ACompanion> OnActivate;
	public Action<ACompanion> OnThrow;
	public Action<ACompanion> OnSelected;
	public Action<ACompanion> OnDeSelected;
	public Action<ACompanion> OnRangeReached;
	public Action<ACompanion> OnStartCharging;
	public Action<ACompanion,float> OnCharging;
	public Action<ACompanion> OnDropped;
	public Action<ACompanion> OnPicked;

	
	public Action<ACompanion> OnChargeFinished;
	
	public abstract void Throw(Vector3 dir);
	public abstract void Activate(GameObject other = null);  //some time the activation requires other objects that companion collides with.
	public abstract void CheckIfOutOfRange();
	public abstract void Reset();
	public abstract void Spawn();
	public abstract void RangeReached();
	public abstract void OnRaycastCollision();
	
	
	public int Index
	{
		get { return m_index; }
		set { m_index = value; }
	}

	public bool IsThrown
	{
		get { return m_isThrown; }
	}

	public float ChargeTime
	{
		get { return m_chargeTime; }
	}

	public bool IsCharged
	{
		get { return m_isCharged; }
		set { m_isCharged = value; }
	}

	public CompanionSteering SteeringComponent
	{
		get { return m_steering; }
		set { m_steering = value; }
	}

	public bool IsInParty
	{
		get { return m_isInParty; }
		set { m_isInParty = value; }
	}

	public Sprite IconSprite
	{
		get { return m_iconSprite; }
	}

	public float ThrowRange
	{
		get { return m_throwRange; }
	}

	public float RespawnTime
	{
		get { return m_respawnTime; }
	}

	public float RespawnCounter
	{
		get { return m_respawnCounter; }
		set { m_respawnCounter = value; }
	}
}
