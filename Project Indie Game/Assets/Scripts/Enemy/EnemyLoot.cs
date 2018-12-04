using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoot : MonoBehaviour {

    [SerializeField]
    private GameObject m_itemToDrop;
	void Start () {
		
	}

    public void DropItem(float percantageOfDroping)
    {
        float rnd=Random.Range(0, 100);
        if (rnd < percantageOfDroping)
        {
            Debug.Log("DROP");
            Instantiate(m_itemToDrop, transform.position, transform.rotation);
        }
        else Debug.Log("FAIL");
    }
}
