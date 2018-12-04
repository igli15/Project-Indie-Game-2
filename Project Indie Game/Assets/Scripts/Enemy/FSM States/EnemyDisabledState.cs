using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDisabledState : AbstractState<EnemyFSM> {

    void Start () {
	}

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        Debug.Log("Welcome back, vegetable");
    }
    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("Bye bye, vegetable");
    }


    void Update () {
		
	}
}
