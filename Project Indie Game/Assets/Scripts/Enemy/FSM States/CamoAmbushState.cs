using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoAmbushState : AbstractState<EnemyFSM>
{
    private Health m_health;
    private Camouflage m_enemy;
    private EnemyFSM m_enemyFSM;

    [SerializeField]
    private float m_timeTransformToAmbsuh = 1;
    [SerializeField]
    private float m_minTimeOfAmbush = 1;
    [SerializeField]
    private float m_timeTransformOutOfAmbush = 1;

    private bool m_isCollidedWithPlayer = false;
    private bool m_isUnderGround = false;
    private bool m_isPlayerOnTopOfMe = false;

    private float m_startTimeOfAmbush;
   
    void Start()
    {
        m_health = GetComponent<Health>();
        m_enemyFSM = GetComponent<EnemyFSM>();
        m_enemy = GetComponent<Camouflage>();
        m_enemy.sphereCollider.OnEnemyTriggerStay += OnSphereTriggerStay;
    }

    void Update()
    {
        if (m_isUnderGround)
        {
            //Debug.Log("TIME " + Time.time+" / "+(m_startTimeOfAmbush + m_minTimeOfAmbush) );
        }
    }

    void HideUnderground()
    {
        transform.position += transform.up * 0.6f;
    }

    void LeaveUnderground()
    {
        transform.position += -transform.up * 0.6f;
    }

    public override void Enter(IAgent pAgent)
    {
        base.Enter(pAgent);
        StopAllCoroutines();
        Debug.Log("ENTER AMBUSH STATE");
        StartCoroutine(TransformToAmbush());
    }

    IEnumerator TransformToAmbush()
    {
        Debug.Log("START Transform to ambush");
        m_isCollidedWithPlayer = true;
        yield return new WaitForSeconds(m_timeTransformToAmbsuh);
        m_startTimeOfAmbush = Time.time;
        m_isUnderGround = true;
        m_isCollidedWithPlayer = false;
        m_health.CanTakeDamage = false;
        Debug.Log("END Transform to ambush: " + Time.time);

        HideFromCompanions(false);

    }

    IEnumerator TransformOutOfAmbush(Transform targetTransform)
    {
        Debug.Log("START Transform OUT OF ambush: " + Time.time);


        m_health.CanTakeDamage = true;
        m_isUnderGround = false;
        HideFromCompanions(true);

        yield return new WaitForSeconds(m_timeTransformOutOfAmbush);

        Vector3 dir = targetTransform.position - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        dir.Normalize();

        Debug.Log("END Transform OUT OF ambush");
        m_enemy.direction = dir;

        m_enemyFSM.fsm.ChangeState<CamoChargeState>();
        yield return null;
    }

    void HideFromCompanions(bool hide)
    {
        GetComponent<BoxCollider>().enabled = hide;
        GetComponent<CapsuleCollider>().enabled = hide;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().useGravity = hide;

        if (hide) HideUnderground();
        else LeaveUnderground();
    }

    public override void Exit(IAgent pAgent)
    {
        base.Exit(pAgent);
        Debug.Log("EXIT AMBUSH STATE");
        StopAllCoroutines();
    }

    void OnSphereTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && enabled && !m_isCollidedWithPlayer)
        {
            //Debug.Log("COLLIDED WITH PLAYER");
            if (m_startTimeOfAmbush + m_minTimeOfAmbush < Time.time)
            {
                m_isCollidedWithPlayer = true;
                Debug.Log("COLLIDED WITH " + collider.name + " /time/ " + Time.time);
                StartCoroutine(TransformOutOfAmbush(collider.transform));
            }
        }

    }

    

    private void OnDrawGizmos()
    {
        if (m_isUnderGround) Gizmos.color = Color.green;
        else Gizmos.color = Color.yellow;
        if (enabled)
            Gizmos.DrawWireSphere(transform.position + transform.up * 1.5f, 1f);
    }
}
