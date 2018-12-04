using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyMoveScript : MonoBehaviour
{
	private Rigidbody m_rb;
	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.V))
		{
			m_rb.velocity = -transform.forward;
		}
	}
}
