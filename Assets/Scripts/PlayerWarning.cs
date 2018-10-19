using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarning : MonoBehaviour {
    public GameObject keyImg;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.activeSelf)
            keyImg.SetActive(false);
	}
}
