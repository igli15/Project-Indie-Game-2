using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public string tag;

    [SerializeField]
    private float m_damage=1;
    private Vector3 m_distancePerFrame;
	void Start () {
        m_distancePerFrame = Vector3.zero;

    }

    public void SetDamage(float damage)
    {
        m_damage = damage;
    }

    public void SetDistancePerFrame(Vector3 distancePerFrame)
    {
        m_distancePerFrame = distancePerFrame;
    }

    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Health>().InflictDamage(m_damage);
            ObjectPooler.instance.DestroyFromPool(tag, gameObject);
        }
        if (!collider.CompareTag("Obstacle") ) return;
        ObjectPooler.instance.DestroyFromPool(tag, gameObject);
    }
}
