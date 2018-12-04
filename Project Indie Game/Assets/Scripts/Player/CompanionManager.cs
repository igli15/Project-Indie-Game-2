using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
	[SerializeField]
	private List<ACompanion> m_companions = new List<ACompanion>();

	private ACompanion m_selectedCompanion;

	private int m_companionCount;

	// Use this for initialization
	void Start ()
	{
		m_companionCount = m_companions.Count;
		SpawnCompanions();

		SelectCompanion(1);
	}

	public void SpawnCompanion(ACompanion companion)
	{
		companion.gameObject.SetActive(true);
		companion.Spawn();
	}

	public void DisableCompanion(ACompanion companion)
	{
		if (companion.OnDisable != null) companion.OnDisable(companion);
		
		companion.gameObject.SetActive(false);
		StartCoroutine(ReSpawnCooldown(companion));

	}
	
	IEnumerator ReSpawnCooldown(ACompanion companion)
	{
		companion.RespawnCounter = 0;

		while (companion.RespawnCounter <= companion.RespawnTime)
		{
			companion.RespawnCounter += Time.deltaTime;
			yield return null;
		}
		
		SpawnCompanion(companion);

		yield return null;
	}

	public ACompanion GetSelectedCompanion()
	{
		return m_selectedCompanion;
	}

	public void SelectNextCompanion()
	{	
		int index = m_selectedCompanion.Index;

		m_selectedCompanion.IsCharged = false;
		if (m_selectedCompanion.Index >= m_companionCount) index = GetTheFirstAvaiableCompanion().Index;
		else index += 1;
		
		ACompanion companion = SelectCompanion(index);
		
		while (companion == null)
		{
			index++;
			if (index >= m_companionCount + 1) index = 1;
			companion = SelectCompanion(index);
		}

		m_selectedCompanion.IsCharged = false;
		
	}

	public void SelectPreviousCompanion()
	{
		int index = m_selectedCompanion.Index;

		m_selectedCompanion.IsCharged = false;
		if (m_selectedCompanion.Index <= 1) index = GetTheLastAvaiableCompanion().Index;
		else index -= 1;

		ACompanion companion = SelectCompanion(index);
		
		while (companion == null)
		{
			index--;
			if (index <= 0) index = m_companionCount;
			companion = SelectCompanion(index);
		}

		m_selectedCompanion.IsCharged = false;
	}

	public ACompanion SelectCompanion(int index)
	{
		ACompanion compToSelect = m_companions[index - 1];	// get the companion we need to change to

		//if (compToSelect.IsThrown) return null;
		
		if (compToSelect != null && !compToSelect.IsThrown) // check if its null and if its thrown
		{

			if (m_selectedCompanion != null && m_selectedCompanion.OnDeSelected != null)  //Call on deselect for the current 
				m_selectedCompanion.OnDeSelected(m_selectedCompanion);

			m_selectedCompanion = m_companions[index - 1];      //Set the companion to selected

			if (m_selectedCompanion.OnSelected != null) m_selectedCompanion.OnSelected(m_selectedCompanion); //call select action
		}
		

		return compToSelect;

	}


	public ACompanion DropCompanion(ACompanion companion)
	{
		companion.Reset();
		companion.IsInParty = false;
		companion.SteeringComponent.NavMeshAgent.enabled = false;
		if(companion.OnDropped != null) companion.OnDropped(companion);
		//m_companionCount -= 1;
		
		m_companions[companion.Index - 1] = null;
		
		//AssignCompanionsIndex();
		SelectPreviousCompanion();
		return companion;
	}

	public ACompanion PickCompanion(ACompanion companion)
	{
		ACompanion droppedCompanion = null;
		if (IsPartyFull())
		{
			droppedCompanion = DropCompanion(m_selectedCompanion);
			
			//AssignCompanionsIndex();
		}
		
		Debug.Log(m_companionCount);
		companion.Spawn();
		companion.IsInParty = true;
		companion.SteeringComponent.NavMeshAgent.enabled = true;
		
		companion.Index = GetEmptySlot();
		m_companions[GetEmptySlot() - 1] = companion;
		
		if(companion.OnPicked != null) companion.OnPicked(companion);
		
		//AssignCompanionsIndex();

		//if (droppedCompanion != null) companion.Index = droppedCompanion.Index;
		
		SelectCompanion(companion.Index);
		return companion;
	}


	private void AssignCompanionsIndex()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			m_companions[i].Index = i+1;
		}
	}
	
	private void SpawnCompanions()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			SpawnCompanion(m_companions[i]);
			m_companions[i].IsInParty = true;
			m_companions[i].Index = i+1;
		}
	}

	public List<ACompanion> Companions
	{
		get { return m_companions; }
	}

	private ACompanion GetTheFirstAvaiableCompanion()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			if (m_companions[i] != null)
			{
				return m_companions[i];
			}
		}

		return null;
	}
	
	
	private ACompanion GetTheLastAvaiableCompanion()
	{
		for (int i = m_companionCount - 1; i >= 0; i -- )
		{
			if (m_companions[i] != null)
			{
				return m_companions[i];
			}
		}

		return null;
	}

	private bool IsPartyFull()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			if (m_companions[i] == null)
			{
				return false;
			}
		}

		return true;
	}


	private int GetEmptySlot()
	{
		for (int i = 0; i < m_companionCount; i++)
		{
			if (m_companions[i] == null)
			{
				return i + 1;
			}
		}

		return -1;
	}

}
