using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayer : MonoBehaviour
{

	[SerializeField]
	private GameObject m_resolutionScreen;

	[SerializeField]
	private SkinnedMeshRenderer m_top;
	
	[SerializeField]
	private SkinnedMeshRenderer m_skirt;

    [SerializeField]
    private Collider m_collider;

	private GameObject m_model;

	private Player m_player;

	private CompanionController m_companionController;

	private CompanionManager m_companionManager;

	private Health m_health;

	private float m_initMoveSpeed;
	
	// Use this for initialization
	void Start ()
	{
		m_player = GetComponent<Player>();
		m_companionController = GetComponent<CompanionController>();
		m_companionManager = GetComponent<CompanionManager>();
		m_health = GetComponent<Health>();

		m_initMoveSpeed = m_player.MoveSpeed;
	}

	public void SetModel(GameObject model)
	{
		m_model = model;
	}
	
	public void Disableplayer()
	{
		if (m_health.GetCurrentHealth <= 0.1f)
		{
			m_resolutionScreen.SetActive(true);
		}

        //m_model.SetActive(false);
        m_collider.enabled = false;
		m_top.enabled = false;
		m_skirt.enabled = false;
		m_player.MoveSpeed = 0;
		m_companionController.enabled = false;
		m_companionManager.enabled = false;
		m_health.CanTakeDamage = false;
	}

	public void ActivatePlayer()
	{
        //m_model.SetActive(true);
        m_collider.enabled = true;
        m_top.enabled = true;
		m_skirt.enabled = true;
		m_player.MoveSpeed = m_initMoveSpeed;
		m_companionController.enabled = true;
		m_companionManager.enabled = true;
		m_health.CanTakeDamage = true;
	}
}
