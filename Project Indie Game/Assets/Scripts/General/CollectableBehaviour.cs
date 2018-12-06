using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour {

    [SerializeField]
    private float m_speedOfRotation=10;

	void Start () {
		
	}
	
	void Update () {
        transform.Rotate(new Vector3(0, m_speedOfRotation*Time.deltaTime, 0));
	}
}
