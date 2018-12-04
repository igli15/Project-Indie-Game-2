using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoChargeState : AbstractState<EnemyFSM>
{

    private Rigidbody m_rigidbody;
    private Camouflage m_enemy;
    private EnemyFSM m_enemyFSM;

    private Vector3 m_direction;
    private Vector3 m_departurePos;

    [SerializeField]
    private float m_damage = 1;
    [SerializeField]
    private float m_pushForce = 2;
    [SerializeField]
    private float m_speed = 2;
    [SerializeField]
    private float m_distance = 3;

    private bool m_isCollidedWithplayer = false;

    void Awake()
    {
        Debug.Log("CHARGE");
        m_direction = Vector3.zero;

        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemy = GetComponent<Camouflage>();
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(m_departurePos, transform.position) > m_distance)
        {
            Debug.Log("STOP");
            m_rigidbody.velocity = Vector3.zero;
            m_enemyFSM.fsm.ChangeState<CamoAmbushState>();
        }
        else
        {
            m_rigidbody.velocity = m_direction * m_speed;
        }
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        //Debug.Log("Enemy is null => " + (m_enemy == null));
        //Debug.Log("RigidBody is null => " + (m_rigidbody == null));
        m_direction = m_enemy.direction;
        Debug.Log("ENTER CHARGE STATE ||| DIR: " + m_direction);

        m_departurePos = transform.position;

        m_rigidbody.velocity = m_direction * m_speed;
        m_isCollidedWithplayer = false;
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("EXIT CHARGE STATE");
    }


    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            m_enemyFSM.fsm.ChangeState<CamoAmbushState>();
        }

        if (collision.collider.CompareTag("Player")&&!m_isCollidedWithplayer)
        {
            Debug.Log("DAMAGE");
            m_isCollidedWithplayer = true;
            Vector3 pushDirection = collision.collider.transform.position - transform.position;
            pushDirection.Normalize();

            collision.collider.GetComponent<Rigidbody>().AddForce(pushDirection * m_pushForce);
            collision.collider.GetComponent<Health>().InflictDamage(m_damage);

            m_enemyFSM.fsm.ChangeState<CamoAmbushState>();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, m_distance);

        Gizmos.color = Color.red;
        if (enabled)
            Gizmos.DrawWireSphere(transform.position + transform.up * 1.5f, 1f);

    }

}
