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
    private float timer;
    private bool startTimer;
    public AreaReached area1;
	// Use this for initialization
	void Start () {
        guide = FindObjectOfType<GuideAi>();
	}
	
	// Update is called once per frame
	void Update () {
        if(guide.stoppedArea == 1 && !launchAxe && !startTimer && area1.reached)
        {
            tutorial.StartTutorialDialogue(0);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1.5f;
            startTimer = true;
            area1.reached = false;
        }

        if(startTimer)
        {
            timer += Time.deltaTime;

            if(timer < 0.2f)
                GameObject.FindGameObjectWithTag("NegroD'agua").GetComponent<Animator>().SetBool("Throw", true);

            if (timer > 0.3f)
            {
                GameObject FdTAxe = (GameObject)Instantiate(startAxe, points[0].transform.position, guide.gameObject.transform.rotation);
                FdTAxe.transform.eulerAngles = new Vector3(0, 0, 69.34f);
                FdTAxe.GetComponent<LaunchItem>().point0 = points[0];
                FdTAxe.GetComponent<LaunchItem>().point1 = points[1];
                FdTAxe.GetComponent<LaunchItem>().point2 = points[2];
                FdTAxe.GetComponent<LaunchItem>().path = starteAxePoint.transform;
                timer = 0;
                GameObject.FindGameObjectWithTag("NegroD'agua").GetComponent<Animator>().SetBool("Throw", false);
                startTimer = false;
                launchAxe = true;
            }
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
