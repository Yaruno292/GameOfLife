using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeClick : MonoBehaviour {

    public static bool active = false;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
    }

    void OnMouseDown()
    {
        if(active == false)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            active = true;
        }
        else if(active == true)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
            active = false;
        }
        
    }
}
