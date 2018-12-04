using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Enemy m_enemy;
    private NavMeshAgent m_navMeshAgent;
    
    private float m_pushPower = 0;

    private float m_initialSpeed;
    private bool m_isPushed = false;
    public bool pushIsEnabled = true;
	void Awake () {

        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_enemy = GetComponent<Enemy>();
	    
        m_enemy.sphereCollider.OnEnemyTriggerEnter += OnObjectTriggerStay;
    }
	
	void Update () {

    }

    void OnObjectTriggerStay(Collider collider)
    {
        if (!pushIsEnabled) return;
        if (collider.CompareTag("Enemy"))
        {
            

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 direction = collider.transform.position - transform.position;

            if (Vector3.Dot(forward, direction) >=0) return;
            //ENEMY IS BEHIND
            
            if (Vector3.Dot(transform.TransformDirection(Vector3.right), direction) >=0)
            {
                //ENEMY IS ON RIGHT SIDE
                direction.Normalize();
                direction += transform.right/2;
            }
            else
            {
                //ENEMY IS ON LEFt SIDE
                direction.Normalize();
                direction -= transform.right / 2;
            }

            
            collider.GetComponent<NavMeshAgent>().velocity += direction * m_pushPower;
        }
    }

    IEnumerator DisablePushingFor(float seconds)
    {
        m_isPushed = true;
        yield return new WaitForSeconds(seconds);
        m_isPushed = false;
        yield return null;
    }

    private void SlowDown()
    {
        if (m_isPushed) return;
        StartCoroutine(DisablePushingFor(1));
        
        m_navMeshAgent.velocity = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    public void SetPushPower(float pushPower)
    {
        m_pushPower = pushPower;
    }
    public void SetMoveSpeed(float moveSpeed)
    {
        m_navMeshAgent.speed = moveSpeed;
        m_initialSpeed = moveSpeed;
    }

    public void WarpToCurrentPosition()
    {
         m_navMeshAgent.Warp(transform.position);
    }
    public void SetDestination(Vector3 destination)
    {
        //Debug.Log("OnNavMrdh: "+m_navMeshAgent.isOnNavMesh);
        
        if (!m_navMeshAgent.isOnNavMesh)
        {
            m_navMeshAgent.Warp(transform.position);
            Debug.Log(" !!! AGENT IS NOT ON NAV_MESH !!! ");
        }
        if(m_navMeshAgent.isOnNavMesh)m_navMeshAgent.SetDestination(destination);
    }
    public void ResetPath()
    {
        m_navMeshAgent.ResetPath();
    }

    public NavMeshAgent navMeshAgent { get { return m_navMeshAgent; } }
}
