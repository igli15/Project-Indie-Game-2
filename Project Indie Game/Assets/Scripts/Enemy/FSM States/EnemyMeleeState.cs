using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMeleeState : AbstractState<EnemyFSM>
{

    private EnemyFSM m_enemyFSM;
    private EnemyMeleeAttack m_enemyMeleeAttack;
    private Rigidbody m_rigidbody;
    private NavMeshObstacle m_navMeshObstacle;

    void Awake()
    {
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_rigidbody = GetComponent<Rigidbody>();
        m_enemyMeleeAttack = GetComponent<EnemyMeleeAttack>();
        m_navMeshObstacle = GetComponent<NavMeshObstacle>();

        m_navMeshObstacle.enabled = false;
        m_enemyMeleeAttack.OnAttackEnds += OnAttackEnds;
    }

    private void OnAttackEnds(bool isPlayerDamaged)
    {
        if (isPlayerDamaged)
        {
            m_enemyMeleeAttack.AttackPlayer();
            StartCoroutine(SetItRed());
        }
        else
        {
            m_enemyFSM.fsm.ChangeState<GoombaSeekState>();
            StartCoroutine(SetItRed());
        }
        
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
       
        m_rigidbody.constraints =  RigidbodyConstraints.FreezeRotationX |
            RigidbodyConstraints.FreezeRotationY| RigidbodyConstraints.FreezeRotationZ;

        m_enemyMeleeAttack.AttackPlayer();
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        StopAllCoroutines();
    }

    IEnumerator SetItRed()
    {   
        yield return new WaitForSeconds(0.5f);
        yield return null;
    }

    void Update()
    {
    }
}
