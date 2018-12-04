using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompedexDescription : MonoBehaviour
{

    private Image m_image;
    private Sprite m_defaultSprite;

    public static CompedexDescription instance;

    void Start()
    {
        m_image = GetComponent<Image>();
        m_defaultSprite = m_image.sprite;

        if (instance == null) instance = this;
    }

    public void SetSprite(Sprite newSprite)
    {
        m_image.sprite = newSprite;
    }
    public void ResetSprite()
    {
        m_image.sprite = m_defaultSprite;
    }
}
