using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField] 
	private Transform m_target;

	[SerializeField] 
	[Range(0,10)]
	private float m_smoothSpeed = 2f;

	[SerializeField] 
	private bool m_lookAtTarget = false;

	private Vector3 m_offset;

	private void Start()
	{
		m_offset = transform.position - m_target.position;
	}

	private void LateUpdate()
	{
		Vector3 desiredPos =  m_target.position + m_offset;
		Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, m_smoothSpeed * Time.deltaTime);
		transform.position = smoothedPos;
		
		if(m_lookAtTarget)
		transform.LookAt(smoothedPos);
	}
}
