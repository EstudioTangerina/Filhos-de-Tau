﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area3 : MonoBehaviour {
    public ArrowTarget[] targets;
    public TutorialManager tutorial;
    private GuideAi guide;
    private PlayerMovement player;
    public GameObject extraArrows;
    public Transform extraArrowsPoint;
    public bool control;
    public Transform[] points = new Transform[3];
    // Use this for initialization
    void Start () {
        guide = FindObjectOfType<GuideAi>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		if(targets[0].hit && targets[1].hit && targets[2].hit && targets[3].hit && targets[4].hit && targets[5].hit && !tutorial.partCompleted[3])
        {
            guide.ChangeWaypoints(0);
            tutorial.partCompleted[3] = true;
            tutorial.StartTutorialDialogue(6);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1.5f;
        }
        else
        {
            if(player.ammo <= 0 && !tutorial.partCompleted[3] && !control)
            {
                GameObject extraArrow = (GameObject)Instantiate(extraArrows, GameObject.FindObjectOfType<GuideAi>().transform.position, guide.gameObject.transform.rotation);
                extraArrow.GetComponent<AddArrow>().isTutorial = true;
                extraArrow.GetComponent<AddArrow>().point0 = points[0];
                extraArrow.GetComponent<AddArrow>().point1 = points[1];
                extraArrow.GetComponent<AddArrow>().point2 = points[2];
                extraArrow.GetComponent<AddArrow>().path = extraArrowsPoint.transform;
                control = true;
                //guide.gameObject.GetComponent<Animator>().SetBool("Launch", true);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tutorial.partCompleted[3])
        {
            guide.ChangeWaypoints(3);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
