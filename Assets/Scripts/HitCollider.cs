using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {
    public bool colliding;
	// Use this for initialization
	void Start () {
        GetComponent<BoxCollider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && colliding == false)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGO = col.gameObject;
            colliding = true;
        }
    }
}
