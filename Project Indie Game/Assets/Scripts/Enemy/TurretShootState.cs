using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShootState : AbstractState<EnemyFSM> {
    private Enemy m_enemy;
    private EnemyFSM m_enemyFSM;
    private EnemyRangedAttack m_rangedAttack;

    private bool m_attackIsAllowed = false;

    [SerializeField]
    private float m_reloadTime = 0.7f;

    [SerializeField]
    private float m_transferTime = 0.5f;

    private void Start()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_rangedAttack = GetComponent<EnemyRangedAttack>();

        m_reloadTime = m_rangedAttack.reloadTime;
        m_enemy.sphereCollider.OnEnemyTriggerExit += OnPlayerExitSpehere;
    }

    private void OnPlayerExitSpehere(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("EXIT OF PLAYER");
            StartCoroutine(MoveToPortableMode(m_transferTime));
  
        }
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        m_attackIsAllowed = false;
        StartCoroutine( MoveToStaticMode(m_transferTime) );
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        StopAllCoroutines();
        m_attackIsAllowed = false;
    }

    void Update () {
        

    }

    IEnumerator Shoot()
    {
        while (m_attackIsAllowed)
        {
            yield return new WaitForSeconds(m_reloadTime);
            m_rangedAttack.ShootTo(m_enemy.target.transform.position,"TurretProjectile");
        }
        yield return null;
    }

    IEnumerator MoveToStaticMode(float timeToTransform)
    {
        m_attackIsAllowed = false;
        yield return new WaitForSeconds(timeToTransform);


        m_attackIsAllowed = true;
        StartCoroutine(Shoot());
        yield return null;
    }

    IEnumerator MoveToPortableMode(float timeToTransform)
    {
        m_attackIsAllowed = false;
        yield return new WaitForSeconds(timeToTransform);
        m_enemyFSM.fsm.ChangeState<TurretSeekState>();
        yield return null;
    }
}
