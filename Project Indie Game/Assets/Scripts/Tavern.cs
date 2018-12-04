using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour {

    [SerializeField]
    private GameObject m_compedexHUD;
	void Start () {
        m_compedexHUD.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Player") )return;
        m_compedexHUD.SetActive(true);
    }
    private void OnTriggerExit(Collider collider)
    {
        if (!collider.CompareTag("Player")) return;
        m_compedexHUD.SetActive(false);
    }
}
