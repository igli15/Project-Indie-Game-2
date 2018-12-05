using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAnimationHandle : MonoBehaviour
{

	private DisablePlayer m_disablePlayer;
	
	
	// Use this for initialization
	void Start ()
	{
		m_disablePlayer = GetComponentInParent<DisablePlayer>();
		m_disablePlayer.SetModel(gameObject);
	}

	public void DisablePlayer()
	{
		m_disablePlayer.Disableplayer();
	}
}
