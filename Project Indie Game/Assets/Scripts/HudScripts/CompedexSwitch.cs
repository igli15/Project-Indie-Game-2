using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompedexSwitch : MonoBehaviour
{

    [SerializeField]
    GameObject m_notes;
    [SerializeField]
    GameObject m_companions;

    void Start()
    {
        m_companions.SetActive(false);
        m_notes.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_notes.SetActive(!m_notes.active);
            m_companions.SetActive(!m_companions.active);
        }
    }
}
