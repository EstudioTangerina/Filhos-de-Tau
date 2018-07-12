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

        if(col && collided)
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.y > transform.position.y)
                GetComponent<SpriteRenderer>().sortingOrder = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder + 1;

            else
                GetComponent<SpriteRenderer>().sortingOrder = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder - 1;
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
        {
            GetComponent<SpriteRenderer>().sortingOrder = -21;
            collided = true;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<LaunchItem>().enabled = false;
            collision.gameObject.GetComponent<Collider2D>().offset = new Vector2(0.57f, 0.28f);
            collision.gameObject.transform.position = this.transform.position;
            collision.gameObject.transform.rotation = this.transform.rotation;
            collision.gameObject.GetComponent<ItemName>().enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirstItens")
        {
            collided = false;
            GetComponent<SpriteRenderer>().sortingOrder = -23;
        }
    }
}
