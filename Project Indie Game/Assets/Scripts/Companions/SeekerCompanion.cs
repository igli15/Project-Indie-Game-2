using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekerCompanion : Companion
{

	[Header("Seeker Companion Values")]
	[SerializeField]
	private float m_seekRange = 3;

	[SerializeField] 
	private float m_bounceAmount = 4;
	
	[SerializeField] 
	private float m_damageDealt = 20;

	private Collider m_collider;

	private float m_initbounceAmount = 0;

	private Transform m_targetTransform;
	
	private void Awake()
	{
		base.Awake();
		m_initbounceAmount = m_bounceAmount;
		m_collider = GetComponent<Collider>();
	}

	// Use this for initialization


	public override void Throw(Vector3 dir)
	{
		base.Throw(dir.normalized);
		m_collider.enabled = true;
	}

	// Update is called once per frame
	void Update () 
	{
		base.Update();

		if (m_targetTransform != null && m_isThrown)
		{
			Vector3 dir = (m_targetTransform.position - transform.position).normalized;
			m_rb.rotation = Quaternion.LookRotation(m_rb.velocity.normalized);
			m_rb.velocity = dir * m_throwSpeed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		Activate(other.gameObject);
	}

	public override void Reset()
	{
		base.Reset();
		m_collider.enabled = false;
		m_bounceAmount = m_initbounceAmount;
		m_targetTransform = null;
	}

	public override void Activate(GameObject other)
	{
		base.Activate(other);
		if (other.CompareTag("Enemy") && IsThrown)
		{
			List<GameObject> enemiesInRange = GetAllEnemiesInRange(other.transform);  //Fill the list

			other.GetComponent<Health>().InflictDamage(m_damageDealt);   //Inflict Damage
			m_bounceAmount -= 1;  							 // it bounced once 

			if (enemiesInRange.Count == 0 || m_bounceAmount <=0)  //Check if there is no enemies or no bounce left
			{
				m_manager.DisableCompanion(this);
			}
			else
			{
				CheckForObstaclesBlock(enemiesInRange,other.transform);    //Check if there is any obstacles in the way if so remove them from the list
				m_targetTransform = GetTheClosestEnemy(enemiesInRange,other.transform);  //Get the closest enemies that are currently on the range
			}
			
	        
			if (m_targetTransform == null)   //Needs to be done after everything. So the target is assigned
			{
				m_manager.DisableCompanion(this);
			}
		}
		else if(other.gameObject.CompareTag("Obstacle")  && IsThrown) //Disable if it hits anything beside the one stated here
		{
			m_manager.DisableCompanion(this);
		}
	}

	private void CheckForObstaclesBlock(List<GameObject> enemiesInRange,Transform other)
	{
		for (int i = 0; i < enemiesInRange.Count; i++)
		{
			RaycastHit hit;
			if (Physics.Raycast(other.position,
				enemiesInRange[i].transform.position - other.position, out hit))
			{
				if (hit.transform.CompareTag("Obstacle"))
				{
					enemiesInRange.Remove(enemiesInRange[i]);
					i--;
				}
			}
		}
	}

	private List<GameObject> GetAllEnemiesInRange(Transform other)
	{
		Collider[] inRangeColliders = Physics.OverlapSphere(transform.position, m_seekRange);
		List<GameObject> enemiesInRange = new List<GameObject>();
		for (int i = 0; i < inRangeColliders.Length; i++)
		{
			if (inRangeColliders[i].gameObject.CompareTag("Enemy") && inRangeColliders[i].transform != other)
			{
				enemiesInRange.Add(inRangeColliders[i].gameObject);
			}
		}

		return enemiesInRange;
	}

	private Transform GetTheClosestEnemy(List<GameObject> enemiesInRange,Transform other)
	{
		Vector3 targetDir = Vector3.forward * m_seekRange * 2;
		Transform targetTransform = null;
		for (int i = 0; i < enemiesInRange.Count; i++)
		{
					
			if (enemiesInRange[i].transform != other)
			{
				if (targetDir.magnitude >=
				    (enemiesInRange[i].transform.position - other.position).magnitude)
				{
							
					targetDir = enemiesInRange[i].transform.position - other.position;
					targetTransform = enemiesInRange[i].transform;
				}
			}
		}

		return targetTransform;
	}
}
