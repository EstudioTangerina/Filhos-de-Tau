using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFuncition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
