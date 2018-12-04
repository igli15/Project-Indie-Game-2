using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleCompanion : Companion
{
	[Header("BlackHole Companion Values")]
	[SerializeField] 
	private GameObject m_blackHole;

	[SerializeField] 
	[Range(0.1f,2)]
	private float m_pullForce = 1;
	
	/*[SerializeField]
	private float m_maxChargeTime = 3;

	[SerializeField]
	private float m_minDistance = 5;
	
	[SerializeField]
	private float m_maxDistance = 12;
	*/


	[SerializeField] 
	private float m_destroyBlackholeTimer = 2;
	
	private bool m_instantiatedBlackHole;
	
	private void Awake()
	{
		base.Awake();
	}

	// Use this for initialization
	void Start ()
	{
		//OnCharging += Charging;
	}
	
	// Update is called once per frame
	void Update () 
	{
		base.Update();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Obstacle") || other.transform.CompareTag("Enemy"))
		{
			if(m_isThrown)
			Activate();
		}
	}

	public override void RangeReached()
	{
		base.RangeReached();
		
		if(m_isThrown)
		Activate();
	}

	public override void Reset()
	{
		base.Reset();
		m_instantiatedBlackHole = false;
	}

	public override void Activate(GameObject other = null)
	{
		base.Activate(other);

		if (m_instantiatedBlackHole == false)
		{
			GameObject blackHole = Instantiate(m_blackHole);
			Destroy(blackHole,m_destroyBlackholeTimer);
			blackHole.transform.position = transform.position;
			blackHole.GetComponent<BlackHole>().PullForce = m_pullForce;
			m_instantiatedBlackHole = true;
		}

	}


	/*public void Charging(ACompanion companion,float chargeCount)
	{
		if (chargeCount > m_maxChargeTime) chargeCount = m_maxChargeTime;
		m_throwRange = chargeCount / m_maxChargeTime * (m_maxDistance - m_minDistance) + m_minDistance;
	}*/

	/*public float MinDistance
	{
		get { return m_minDistance; }
	}

	public float MaxDistance
	{
		get { return m_maxDistance; }
	}

	public float MaxChargeTime
	{
		get { return m_maxChargeTime; }
	}*/
	
}
