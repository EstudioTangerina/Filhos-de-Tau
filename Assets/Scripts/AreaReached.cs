using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaReached : MonoBehaviour {
    public bool reached;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            reached = true;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
