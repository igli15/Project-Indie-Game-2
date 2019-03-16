using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUISprite : MonoBehaviour
{
	[SerializeField] 
	private float m_rotationSpeed = 0.5f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.forward,m_rotationSpeed * Time.deltaTime);
	}
}
