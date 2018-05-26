using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area1 : MonoBehaviour {
    public RagDool firstRagdoll;
    public TutorialManager tutorial;
    private GuideAi guide;
    public Transform[] points = new Transform[3];
    public GameObject startAxe;
    public Transform starteAxePoint;
    private bool launchAxe;
	// Use this for initialization
	void Start () {
        guide = FindObjectOfType<GuideAi>();
	}
	
	// Update is called once per frame
	void Update () {
        if(guide.stoppedArea == 1 && !launchAxe)
        {
            GameObject FdTAxe = (GameObject)Instantiate(startAxe, GameObject.FindObjectOfType<GuideAi>().transform.position, guide.gameObject.transform.rotation);
            FdTAxe.transform.eulerAngles = new Vector3(0, 0, 69.34f);
            FdTAxe.GetComponent<LaunchItem>().point0 = points[0];
            FdTAxe.GetComponent<LaunchItem>().point1 = points[1];
            FdTAxe.GetComponent<LaunchItem>().point2 = points[2];
            FdTAxe.GetComponent<LaunchItem>().path = starteAxePoint.transform;

            launchAxe = true;
        }

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
