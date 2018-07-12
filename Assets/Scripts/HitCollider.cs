using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {
    public bool colliding;
	// Use this for initialization
	void Start () {
        GetComponent<Collider2D>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Contains(col.gameObject))
        {
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGO == null)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGO = col.gameObject;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Add(col.gameObject);
            colliding = true;
        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Contains(col.gameObject))
        {
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGO == null)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGO = col.gameObject;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Add(col.gameObject);
            colliding = true;
        }
    }
}
