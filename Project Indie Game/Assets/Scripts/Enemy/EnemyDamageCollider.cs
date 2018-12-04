using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour {

    public Action<Collider> OnEnemyTriggerEnter;
    public Action<Collider> OnEnemyTriggerStay;
    public Action<Collider> OnEnemyTriggerExit;

    private void OnTriggerEnter(Collider collider)
    {
        if (OnEnemyTriggerEnter != null) OnEnemyTriggerEnter(collider);
    }
    private void OnTriggerStay(Collider collider)
    {
        if (OnEnemyTriggerStay != null) OnEnemyTriggerStay(collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        if (OnEnemyTriggerExit != null) OnEnemyTriggerExit(collider);

    }
}
