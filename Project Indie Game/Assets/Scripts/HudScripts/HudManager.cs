using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
	[SerializeField] 
	private Sprite m_noneSelectedIcon;
	
	[SerializeField] 
	private CompanionManager m_companionManager;
	
	[SerializeField]
	[Range(1,2)]
	private float m_scaleFactor = 1.2f;

	[SerializeField] 
	private float m_scaleTime = 0.2f;

	[Header("Icons")] 
	[SerializeField] 
	private List<GameObject> m_iconGameObjects = new List<GameObject>();
	
	// Use this for initialization
	void Start () 
	{
		
		for (int i = 0; i < m_companionManager.Companions.Count; i++)
		{
			m_iconGameObjects[i].GetComponent<Image>().sprite = m_companionManager.Companions[i].IconSprite;
		}

		for (int i = 0; i < ACompanion.AllCompanions.Count; i++)
		{
			ACompanion.AllCompanions[i].OnSelected += SelectIcon;
			ACompanion.AllCompanions[i].OnDeSelected += DeselectIcon;
			ACompanion.AllCompanions[i].OnPicked += AssignIcon;
			ACompanion.AllCompanions[i].OnDropped += ResetIcon;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void AssignIcon(ACompanion companion)
	{
		m_iconGameObjects[companion.Index - 1].GetComponent<Image>().sprite = companion.IconSprite;
	}

	public void ResetIcon(ACompanion companion)
	{
		m_iconGameObjects[companion.Index - 1].GetComponent<Image>().sprite = m_noneSelectedIcon;
	}

	public void SelectIcon(ACompanion companion)
	{
		m_iconGameObjects[companion.Index - 1].transform.DOScale(m_scaleFactor,m_scaleTime);
		m_iconGameObjects[companion.Index - 1].GetComponentInChildren<RespawnIconScript>().AssignCompanion(companion);
		m_iconGameObjects[companion.Index - 1].transform.GetChild(0).gameObject.SetActive(true);
		
	}
	public void DeselectIcon(ACompanion companion)
	{
		m_iconGameObjects[companion.Index - 1].transform.DOScale(1, m_scaleTime);
		m_iconGameObjects[companion.Index - 1].transform.GetChild(0).gameObject.SetActive(false);
	}

}
