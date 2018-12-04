using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour, IAgent {

    static int countID=0;
    public int ID;

    private Fsm<EnemyFSM> m_fsm;
    public enum EnemyType {GOOMBA,TURRET,CAMOUFLAGE }
    public EnemyType enemyTpe;

    void Start() {
        ID = countID;
        countID++;
        m_fsm = new Fsm<EnemyFSM>(this);
        ChangeToInitialState();


    }

    public void ChangeToInitialState()
    {
        Debug.Log("ChangeToInitialState");
        switch (enemyTpe)
        {
            case EnemyType.GOOMBA:
                m_fsm.ChangeState<GoombaSeekState>();
                break;
            case EnemyType.TURRET:
                m_fsm.ChangeState<TurretSeekState>();
                break;
            case EnemyType.CAMOUFLAGE:
                m_fsm.ChangeState<CamoAmbushState>();
                break;
        }
    }

    void Update() {
    }

    public Fsm<EnemyFSM> fsm{ get{ return m_fsm; } }

    private void OnDrawGizmos()
    {
        if (fsm == null) return;
        if (fsm.GetCurrentState() is EnemySeekState) Gizmos.color = Color.cyan;
        if (fsm.GetCurrentState() is EnemyMeleeState) Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + transform.up * 2, 0.4f);
    }

    void OnEnable()
    {
        Debug.Log("OnEnabled");
        m_fsm = new Fsm<EnemyFSM>(this);
        ChangeToInitialState();
    }
    
}
