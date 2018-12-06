using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntranceCollider : MonoBehaviour {

    [SerializeField]
    private UnityEvent OnEnter;
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter.Invoke();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
