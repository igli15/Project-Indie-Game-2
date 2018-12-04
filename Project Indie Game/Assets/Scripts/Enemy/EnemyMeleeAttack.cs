using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMeleeAttack : MonoBehaviour
{
    public Action<bool> OnAttackEnds;

    private Enemy m_enemy;

    [SerializeField]
    private float m_damage = 1;
    private float m_reloadTime = 2f;
    private float m_lastTimeAttacked;

    private bool m_attack = false;
    private bool m_playerThere = false;
    void Start()
    {
        m_enemy = GetComponent<Enemy>();
        m_enemy.damageCollider.OnEnemyTriggerStay += OnEnemyTriggerStay;

        ResetWaitingTime();
    }
  
    public void ResetWaitingTime()
    {
        m_lastTimeAttacked = Time.time;
    }

    void OnEnemyTriggerStay(Collider collider)
    {
        m_playerThere = collider.CompareTag("Player");

        if (m_attack && m_playerThere)
        {
            collider.GetComponent<Health>().InflictDamage(m_damage);
            m_attack = false;
        }
    }

    public void AttackPlayer()
    {
        StartCoroutine(WaitTillAttack());
    }

    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(reloadTime);
        m_attack = true;
        yield return new WaitForFixedUpdate();
        if (OnAttackEnds != null) OnAttackEnds(m_playerThere);
        m_playerThere = false;
        yield return null;
    }

    void Update()
    {
        
    }

    public float reloadTime { get { return m_reloadTime; } set { m_reloadTime = value; } }

}

