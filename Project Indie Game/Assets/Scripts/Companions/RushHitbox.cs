using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushHitbox : MonoBehaviour
{

	[SerializeField] 
	private float m_collisionCheckOffset = 0.5f;
	
	private RushCompanion m_parentCompanion;
	
	// Use this for initialization
	void Start ()
	{
		m_parentCompanion = transform.parent.GetComponent<RushCompanion>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		RaycastHit hit;
		//transform.up since the plane is rotated.
		if (Physics.Raycast(transform.position, transform.up, out hit,m_collisionCheckOffset))
		{
			//Debug.Log(hit.transform.gameObject);
			if (hit.transform.CompareTag("Obstacle"))
			{
				m_parentCompanion.Activate();
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (m_parentCompanion.IsThrown)
		{
			if (other.CompareTag("Enemy"))
			{
				m_parentCompanion.CatchEnemy(other.gameObject);
			}
		}
	}
}
