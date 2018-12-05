using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : Enemy {
    
    public Vector3 direction = Vector3.zero;
    private Health m_health;

	void Start () {
        m_health=GetComponent<Health>();
        m_health.OnDeath += OnMyDestroy;
        direction = Vector3.zero;
	}

    public void OnMyDestroy(Health health)
    {
        Debug.Log("HELLO");
        GetComponent<EnemyFSM>().fsm.ChangeState<EnemyDisabledState>();
        animator.SetBool("death", true);
        StartCoroutine(WaitBeforeDestroy(1));
    }

    IEnumerator WaitBeforeDestroy(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Destroy(gameObject);
    }

	void Update () {
		
	}


}
