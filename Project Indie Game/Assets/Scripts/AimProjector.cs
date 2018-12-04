using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimProjector : MonoBehaviour
{
	[SerializeField] 
	private Material m_seekerAimMat;
	
	[SerializeField] 
	private Material m_rusherAimMat;
	
	[SerializeField] 
	private Material m_sniperAimMat;
	
	[SerializeField] 
	private Material m_blackholeAimMat;

	private Projector m_projector;
	
 	// Use this for initialization
	void Start ()
	{
		m_projector = GetComponent<Projector>();

		for (int i = 0; i < ACompanion.AllCompanions.Count; i++)
		{
			ACompanion.AllCompanions[i].OnSelected += ChangeAimMat;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void ChangeAimMat(ACompanion companion)
	{
		if (companion == null)
		{
			return;
		}
		
		if (companion is SeekerCompanion)
		{
			m_projector.orthographicSize = 3;
			m_projector.material = m_seekerAimMat;
		}
		else if (companion is RushCompanion)
		{
			m_projector.orthographicSize = 3;
			m_projector.material = m_rusherAimMat;
		}
		else if (companion is SniperCompanion)
		{
			m_projector.orthographicSize = 3;
			m_projector.material = m_sniperAimMat;
		}
		else if (companion is BlackHoleCompanion)
		{
			m_projector.orthographicSize = 6;
			m_projector.material = m_blackholeAimMat;
		}
	}
	
}
