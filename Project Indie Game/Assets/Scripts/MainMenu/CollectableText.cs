using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableText : MonoBehaviour
{
	private TextMeshProUGUI m_text;

	// Use this for initialization
	void Awake ()
	{
		m_text = GetComponent<TextMeshProUGUI>();
	}

	private void OnEnable()
	{
		m_text.text = Collectable.index + "/" + Collectable.total;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
