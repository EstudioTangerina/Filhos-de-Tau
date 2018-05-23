using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRoll : MonoBehaviour {
    private bool passed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
        public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !passed)
        {
            FindObjectOfType<TutorialManager>().canRoll = true;
            passed = true;
        }
    }
}
