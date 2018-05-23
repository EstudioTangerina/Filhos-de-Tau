using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    public bool rolled;
    public bool col;
    private Animator player;
    public Area4 area4;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (col && player.GetBool("Roll") && !rolled)
        {
            rolled = true;
            area4.logsRolled += 1;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            col = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            col = false;
        }
    }
}
