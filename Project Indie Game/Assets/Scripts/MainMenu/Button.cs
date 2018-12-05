using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
	[SerializeField] 
	private float m_brushTime = 0.45f;

	private UnityEngine.UI.Button m_button;
	
	private Image m_brushImage;

	private Tweener m_brushTweener;
	
	// Use this for initialization
	void Start ()
	{
		m_button = GetComponent<UnityEngine.UI.Button>();
		m_brushImage = GetComponentInChildren<Image>();
		
	}
	

	void ShowBrush()
	{
		m_brushTweener = m_brushImage.DOFillAmount(1, m_brushTime);
	}

	void DisableBrush()
	{
		m_brushTweener.Kill();
		m_brushImage.DOFillAmount(0, m_brushTime);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ShowBrush();
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		DisableBrush();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
