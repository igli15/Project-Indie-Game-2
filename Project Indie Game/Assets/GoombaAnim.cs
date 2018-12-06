using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAnim : MonoBehaviour {

    [SerializeField]
    AudioSource m_audioSource;
	void Start () {
		
	}

    public void PlaySound()
    {
        m_audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
