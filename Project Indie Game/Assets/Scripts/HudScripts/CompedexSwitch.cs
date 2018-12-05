using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompedexSwitch : MonoBehaviour
{

    [SerializeField]
    GameObject m_notes;
    [SerializeField]
    GameObject m_companions;
    [Header("BUTTONS")]
    [SerializeField]
    Button m_button_notes;
    [SerializeField]
    Button m_button_compadex;

    void Start()
    {
        m_companions.SetActive(false);
        m_notes.SetActive(true);
        m_button_notes.onClick.AddListener(SwitchToNotes);
        m_button_compadex.onClick.AddListener(SwitchToCompadex);
    }

    public void SwitchToNotes()
    {
        m_notes.SetActive(true);
        m_companions.SetActive(false);
    }

    public void SwitchToCompadex()
    {
        m_notes.SetActive(false);
        m_companions.SetActive(true);
    }

    public void SwitchTabs()
    {
        m_notes.SetActive(!m_notes.active);
        m_companions.SetActive(!m_companions.active);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTabs();
        }
    }
}
