using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreListenerPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<AudioSource>().ignoreListenerPause = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
