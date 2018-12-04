using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
	private float m_pullForce ;

	private List<GameObject> m_enemiesInRange = new List<GameObject>();

	

	
	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			m_enemiesInRange.Add(other.gameObject);
			other.GetComponent<EnemyFSM>().fsm.ChangeState<EnemyDisabledState>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			m_enemiesInRange.Remove(other.gameObject);
			other.GetComponent<EnemyFSM>().fsm.ChangeState<EnemySeekState>();
		}
		

	}

	private void OnDestroy()
	{
		foreach (GameObject enemy in m_enemiesInRange)
		{
			enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
			Debug.Log("release  enemy");
			enemy.GetComponent<EnemyFSM>().fsm.ChangeState<GoombaSeekState>();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.transform.CompareTag("Enemy"))
		{
			if (Vector3.Distance(transform.position, other.transform.position) <= 0.9f)
			{
				other.GetComponent<Rigidbody>().velocity = Vector3.zero;
			}
			else
			{
				other.GetComponent<Rigidbody>().velocity = (transform.position - other.transform.position).normalized * m_pullForce;
			}
		}
	}

	public float PullForce
	{
		get { return m_pullForce; }
		set { m_pullForce = value; }
	}
}
