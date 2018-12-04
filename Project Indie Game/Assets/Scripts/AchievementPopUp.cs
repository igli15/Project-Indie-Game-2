using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopUp : MonoBehaviour
{
	[Header("PopupTransitionValues")] 
	[SerializeField]
	private float transitionTime = 0.8f;
	
	[SerializeField]
	private float timeTillGone = 2f;
	
	[Header("PopUpElements")] 
	[SerializeField]
	private Text titleText;

	[SerializeField]
	private Text description;

	[SerializeField] 
	private Image mainImage;
	
	[SerializeField] 
	private Image backgroundImage;
	
	[Serializable]
	public class AchievementData
	{
		public string achievementName;
		public string title;
		public string description;
		public Sprite mainImage;
		//public Sprite icon;
		public Sprite background;

		[HideInInspector] 
		public bool isCompleted;
	}

	[Space]
	[Header("PopUpDatas")]
	[SerializeField] private List<AchievementData> achievementData;

	private static Dictionary<string, AchievementData> achievementDictionary;
	private RectTransform rectTransform;
	
	private static Queue<AchievementData> achievementQueue = new Queue<AchievementData>();

	private bool isDisplaying = false;

		
	// Use this for initialization
	void Start ()
	{
		
		achievementDictionary = new Dictionary<string, AchievementData>();
		rectTransform = GetComponent<RectTransform>();
		rectTransform.anchoredPosition = new Vector2(0 + rectTransform.sizeDelta.x, 0);

		foreach (AchievementData data in achievementData)
		{
			achievementDictionary.Add(data.achievementName,data);
		}
	}


	private void Show(AchievementData pData)
	{
		if (pData.background != null)
		{
			backgroundImage.sprite = pData.background;
		}

		if (pData.mainImage != null)
		{
			mainImage.enabled = true;
			mainImage.sprite = pData.mainImage;
		}
		else
		{
			mainImage.enabled = false;
		}

		if (pData.description != null)
		{
			description.text = pData.description;
		}

		if (pData.title != null)
		{
			titleText.text = pData.title;
		}


		rectTransform.DOAnchorPos(new Vector2(0, 0),transitionTime);

		isDisplaying = true;
		StartCoroutine(Reset(pData));
	}

	IEnumerator Reset(AchievementData pData)
	{
		yield return  new WaitForSeconds(timeTillGone);
		
		rectTransform.DOAnchorPos(new Vector2(0 + rectTransform.sizeDelta.x, 0), transitionTime);
		DOVirtual.DelayedCall(transitionTime,() => isDisplaying =false);
	}

	private void Update()
	{
		if (achievementQueue.Count > 0)
		{
			if (!isDisplaying)
			{
				Show(achievementQueue.Dequeue());
			}
		}
	}

	public static void QueueAchievement(string achievementName)
	{
		if (!achievementDictionary[achievementName].isCompleted)
		{
			achievementDictionary[achievementName].isCompleted = true;
			achievementQueue.Enqueue(achievementDictionary[achievementName]);
		}
	}

	public static void ResetAchievement(string achievementName)
	{
		achievementDictionary[achievementName].isCompleted = false;
	}

	public static AchievementData GetAchievement(string achievementName)
	{
		if (achievementDictionary.ContainsKey(achievementName))
		{
			return achievementDictionary[achievementName];
		}
		else
		{
			throw new Exception("There is no Achievement with that name");
		}
	}

}
