using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaSeekState : EnemySeekState {
    private Enemy m_enemy;
    private EnemyFSM m_enemyFSM;
    private EnemyMovement m_enemyMovement;
    private EnemyMeleeAttack m_enemyMeleeAttack;

    private GameObject m_seekTarget;


    void Awake()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemyMovement = GetComponent<EnemyMovement>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();

        m_enemy.damageCollider.OnEnemyTriggerEnter += OnPlayerEntersAttackZone;
        m_enemy.damageCollider.OnEnemyTriggerStay += OnPlayerEntersAttackZone;
    }


    private void OnPlayerEntersAttackZone(Collider collider)
    {
        if (collider.CompareTag("Player")) m_enemyFSM.fsm.ChangeState<EnemyMeleeState>();
    }

    public override void Enter(IAgent pAgent)
    {

        base.Enter(pAgent);
        //OK, so Warp to own position is fuckking updating navMeshPos and puting agent on navmesh
        m_enemyMovement.navMeshAgent.Warp(transform.position);

        m_enemyMovement.navMeshAgent.enabled = true;
        m_enemyMovement.pushIsEnabled = true;
        m_seekTarget = m_enemy.target;
        if ((m_seekTarget.transform.position - transform.position).magnitude < 0.1f)
        {

            m_enemyFSM.fsm.ChangeState<EnemyMeleeState>();
            return;
        }
        StartCoroutine(FollowTarget(m_seekTarget.transform,m_enemyMovement));


    }

    public override void Exit(IAgent pAgent)
    {

        base.Exit(pAgent);

        //SLOW DOWN BEFORE ATTACK, KEK
        m_enemyMovement.navMeshAgent.velocity = Vector3.zero;
        //GetComponent<Rigidbody>().velocity = Vector3.zero;

        StopAllCoroutines();

        m_enemyMovement.ResetPath();
        m_enemyMovement.navMeshAgent.enabled = false;
        m_enemyMovement.pushIsEnabled = false;
    }

}
