using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour {


    [SerializeField]
    private GameObject m_projectile;

    [SerializeField]
    private float m_projectileSpeed = 5;

    [HideInInspector]
    public float reloadTime = 1.5f;

    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShootTo(Vector3 targetPosition,string objectPoolTag)
    {
        Vector3 direction=targetPosition- transform.position;
        direction.Normalize();

        GameObject newProjectile = ObjectPooler.instance.SpawnFromPool
            (objectPoolTag, transform.position + transform.up, transform.rotation);

        newProjectile.GetComponent<ProjectileBehaviour>().tag = objectPoolTag;
        Rigidbody rb =newProjectile.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.velocity = direction * m_projectileSpeed;


    }
}
