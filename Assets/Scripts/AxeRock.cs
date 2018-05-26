using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeRock : MonoBehaviour {
    private bool collided;
    private bool col;
    public Sprite broken;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (collided && !col)
        {
            GetComponent<Animator>().enabled = true;
            col = true;
        }

        if(!collided && col)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = broken;
        }
        
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirstItens")
            collided = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirstItens")
            collided = false;
    }
}
