using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claws : MonoBehaviour {
    private bool controle;
    private int dmg;
    private float slow;
    private float slowTime;
	// Use this for initialization
	void Start () {
        controle = true;
        dmg = 5;
        slow = 0.5f;
        slowTime = 8;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && controle)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(dmg);
            collision.GetComponent<PlayerMovement>().SpeedDown(slow, slowTime);
            controle = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && controle)
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(dmg);
            collision.GetComponent<PlayerMovement>().SpeedDown(slow, slowTime);
            controle = false;
        }

    }
}
