using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {


    [HideInInspector]
    public static EnemyManager instance;
    
    public static Action OnNextWave;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CallNextWave()
    {
        Debug.Log("Called global NEXT_WAVE");
        if (OnNextWave != null) OnNextWave();
    }

    void Start () {
    }
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.K)) CallNextWave();
    }
}
