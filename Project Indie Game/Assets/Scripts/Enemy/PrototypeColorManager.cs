using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrototypeColorManager : MonoBehaviour {



    void Start () {
        GetComponent<Renderer>().material.color = Color.green;
	}

    public void ChangeColorTo(Color newColor)
    {
        GetComponent<Renderer>().material.color = newColor;
    }
}
