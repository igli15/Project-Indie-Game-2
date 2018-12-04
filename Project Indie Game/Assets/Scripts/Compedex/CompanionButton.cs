using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CompanionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Sprite m_description;

    [SerializeField]
    private GameObject m_highLighter;

    private Button m_button;

    void Start()
    {
        m_button = GetComponent<Button>();
    }

    public void Select()
    {
        if (!m_highLighter.active)
        {
            m_highLighter.transform.position = transform.position;
            m_highLighter.SetActive(true);
            CompedexDescription.instance.SetSprite(m_description);
        }
    }

    public void Deselect()
    {
        m_highLighter.SetActive(false);
        CompedexDescription.instance.ResetSprite();
    }

    public void OnEnable()
    {
        Deselect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse is NOT over GameObject.");
        Deselect();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Select();
        Debug.Log("Mouse is over GameObject.");
    }
}
