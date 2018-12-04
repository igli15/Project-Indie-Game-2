using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{
	[SerializeField] 
	private Sprite m_defaultCursor;

	[SerializeField] 
	private Sprite m_seekerCursor;

	[SerializeField]
	private Sprite m_rushCursor;
	
	[SerializeField]
	private Sprite m_sniperCursor;
	
	[SerializeField]
	private Sprite m_blackHoleCursor;

	private Image m_image;
	
	// Use this for initialization
	void Start ()
	{
		UnityEngine.Cursor.visible = false;

		
		m_image = GetComponent<Image>();
		for (int i = 0; i < ACompanion.AllCompanions.Count; i++)
		{
			ACompanion.AllCompanions[i].OnSelected += ChangeCursor;
		}
	}

	private void Update()
	{
		Vector2 pos = Input.mousePosition;
		transform.position = pos;
	}

	private void ChangeCursor(ACompanion companion)
	{
		if (companion == null)
		{
			m_image.sprite = m_defaultCursor;
			return;
		}
		
		if (companion is SeekerCompanion)
		{
			m_image.sprite = m_seekerCursor;
		}
		else if (companion is RushCompanion)
		{
			m_image.sprite = m_rushCursor;
		}
		else if (companion is SniperCompanion)
		{
			m_image.sprite = m_sniperCursor;
		}
		else if (companion is BlackHoleCompanion)
		{
			m_image.sprite = m_blackHoleCursor;
		}
	}
	
	
}
