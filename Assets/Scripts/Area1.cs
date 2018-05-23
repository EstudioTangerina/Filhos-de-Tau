using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area1 : MonoBehaviour {
    public RagDool firstRagdoll;
    public TutorialManager tutorial;
    private GuideAi guide;
	// Use this for initialization
	void Start () {
        guide = FindObjectOfType<GuideAi>();
	}
	
	// Update is called once per frame
	void Update () {
        if (firstRagdoll.attack && !tutorial.partCompleted[1])
        {
            tutorial.partCompleted[1] = true;
            tutorial.StartTutorialDialogue(1);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tutorial.partCompleted[1])
        {
            guide.ChangeWaypoints(1);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
