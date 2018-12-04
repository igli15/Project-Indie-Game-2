using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDamage : MonoBehaviour {

    public SphereCollider collider;

    public float damage = 10;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<Health>().InflictDamage(damage);
        
    }
}
