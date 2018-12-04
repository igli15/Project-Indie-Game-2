using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDamage : MonoBehaviour {

    public SphereCollider collider;

    public float damage = 10;
    bool m_attack=false;

	void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_attack = true;
            transform.position += transform.forward * 0.1f;
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Enemy")) return;

        other.GetComponent<Health>().InflictDamage(damage);
        m_attack = false;
    }
}
