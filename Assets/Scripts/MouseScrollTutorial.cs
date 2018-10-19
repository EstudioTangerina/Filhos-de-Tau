using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScrollTutorial : MonoBehaviour {
    private float counter = 1.5f;
    private bool moved;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (counter > 0)
            counter -= Time.deltaTime;

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            moved = true;

        if (moved && counter <= 0)
            gameObject.SetActive(false);
    }
}
