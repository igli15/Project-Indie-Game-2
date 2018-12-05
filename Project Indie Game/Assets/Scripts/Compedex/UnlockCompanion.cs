using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockCompanion : MonoBehaviour {

    [SerializeField]
    private Sprite m_unlockedIcon;

    private Image m_image;
    private CompanionButton m_button;

	void Start () {
        m_image = GetComponent<Image>();
        m_button = GetComponent<CompanionButton>();
        m_button.enabled = false;
    }

    public void UnlockIcon()
    {
        m_image.sprite = m_unlockedIcon;
        m_button.enabled = true;
    }
	
}
