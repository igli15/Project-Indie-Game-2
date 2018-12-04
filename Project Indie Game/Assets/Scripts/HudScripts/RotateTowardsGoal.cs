using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsGoal : MonoBehaviour
{

	[SerializeField] 
	private Transform m_playerTransform;
	
	[SerializeField] 
	private Transform m_goalTransform;

	[SerializeField] 
	private float m_rotationSpeed = 3;
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 goalScreenPos = Camera.main.WorldToScreenPoint(m_goalTransform.position);
		Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(m_playerTransform.position);
		Vector2 dir = (goalScreenPos - playerScreenPos).normalized;
		
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion finalRotation = Quaternion.AngleAxis(angle, Vector3.forward);
		
		transform.rotation = Quaternion.RotateTowards(transform.rotation,finalRotation,m_rotationSpeed * Time.deltaTime);
		
	}
}
