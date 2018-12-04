using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSeekState : EnemySeekState {

    private Enemy m_enemy;
    private EnemyFSM m_enemyFSM;
    private EnemyMovement m_enemyMovement;

    private GameObject m_seekTarget;


    void Awake()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemyMovement = GetComponent<EnemyMovement>();

        m_seekTarget = m_enemy.target;
        m_enemy.sphereCollider.OnEnemyTriggerStay += OnPlayerStaySpehere;
    }

    private void OnPlayerStaySpehere(Collider collider)
    {
        if (collider.CompareTag("Player")) m_enemyFSM.fsm.ChangeState<TurretShootState>();
    }

    public override void Enter(IAgent pAgent)
    {
        Debug.Log("ENTER SEEK STATE");
        base.Enter(pAgent);
        m_enemyMovement.navMeshAgent.enabled = true;
        m_enemyMovement.pushIsEnabled = true;
        m_seekTarget = m_enemy.target;

        StartCoroutine(FollowTarget(m_seekTarget.transform, m_enemyMovement));
    }

    public override void Exit(IAgent pAgent)
    {
        
        base.Exit(pAgent);

        //SLOW DOWN BEFORE ATTACK, KEK
        m_enemyMovement.navMeshAgent.velocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        StopAllCoroutines();

        m_enemyMovement.ResetPath();
        m_enemyMovement.navMeshAgent.enabled = false;
        m_enemyMovement.pushIsEnabled = false;
    }
}
