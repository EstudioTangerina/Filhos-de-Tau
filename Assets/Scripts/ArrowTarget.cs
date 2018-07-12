using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTarget : MonoBehaviour {
    public bool hit;
    public int hits;
    public Sprite wasHit;
    public Sprite wasHitL;
    public Sprite wasHitR;
    public GameObject target;
    public GameObject shaddow;
    public Sprite shaddowHit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            hits++;
            hit = true;

            if (collision.gameObject.transform.rotation.eulerAngles.z >= 0 && collision.gameObject.transform.rotation.eulerAngles.z < 45)
            {
                target.GetComponent<SpriteRenderer>().sprite = wasHitL;
                shaddow.GetComponent<SpriteRenderer>().sprite = shaddowHit;
            }

            else if (collision.gameObject.transform.rotation.eulerAngles.z > 135 && collision.gameObject.transform.rotation.eulerAngles.z <= 179)
            {
                target.GetComponent<SpriteRenderer>().sprite = wasHitR;
                shaddow.GetComponent<SpriteRenderer>().sprite = shaddowHit;
            }

            else
            {
                target.GetComponent<SpriteRenderer>().sprite = wasHit;
                shaddow.GetComponent<SpriteRenderer>().sprite = shaddowHit;
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
        }
    }
}
