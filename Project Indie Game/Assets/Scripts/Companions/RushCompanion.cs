using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushCompanion : Companion {

	// Use this for initialization

	[Header("Rush Companion Values")]

	[SerializeField] 
	private GameObject m_rushHitbox;

	[SerializeField] 
	[Range(0,100)]
	private float m_damageAmount = 50;
	
	private Collider m_collider;

	private List<GameObject> m_enemiesCaught = new List<GameObject>();

	private void Awake()
	{
		m_collider = GetComponent<Collider>();
		base.Awake();
	}

	public override void Throw(Vector3 dir)
	{
		base.Throw(dir.normalized);
		m_rushHitbox.SetActive(true);
		m_collider.enabled = true;
	}

	public override void Reset()
	{
		base.Reset();
		m_enemiesCaught.Clear();
		m_rushHitbox.SetActive(false);
		m_collider.enabled = false;
	}


    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (m_isThrown)
        {
            for (int i = 0; i < m_enemiesCaught.Count; i++)
            {
                m_enemiesCaught[i].transform.position = transform.position;
            }
        }
    }

	public override void RangeReached()
	{
		base.RangeReached();
		Activate();
	}

	public void CatchEnemy(GameObject enemy)
	{
        enemy.GetComponent<EnemyFSM>().fsm.ChangeState<EnemyDisabledState>();
		m_enemiesCaught.Add(enemy);
  

        Debug.Log("enemy caught " + enemy.name);
	}


	public override void Activate(GameObject other = null)
	{
		base.Activate(other);

		if (m_isThrown)
		{
			for (int i = 0; i < m_enemiesCaught.Count; i++)
			{
				if (m_enemiesCaught[i] != null)
				{
					m_enemiesCaught[i].GetComponent<Health>().InflictDamage(m_damageAmount);
                    m_enemiesCaught[i].GetComponent<EnemyFSM>().ChangeToInitialState();

                }
			}

			m_manager.DisableCompanion(this);
		}
	}
}
