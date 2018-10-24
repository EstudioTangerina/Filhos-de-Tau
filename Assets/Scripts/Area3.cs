using System.Collections;
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
    private float waitTimer;
    private bool startTimer;
    public GameObject[] itens = new GameObject[2];
    public AreaReached area3;
    // Use this for initialization
    void Start () {
        guide = FindObjectOfType<GuideAi>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if(guide.stoppedArea == 3 && area3.reached)
        {
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 2f;
            tutorial.StartTutorialDialogue(5);
            itens[0].GetComponent<Collider2D>().enabled = true;
            itens[1].GetComponent<Collider2D>().enabled = true;
            area3.reached = false;
        }


		if(targets[0].hit && targets[1].hit && targets[2].hit && targets[3].hit && targets[4].hit && !tutorial.partCompleted[3])
        {
            guide.ChangeWaypoints(0);
            if(FindObjectOfType<GameManager>() != null)
                FindObjectOfType<GameManager>().areaReached = 3;

            tutorial.partCompleted[3] = true;
            tutorial.StartTutorialDialogue(6);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1.5f;
        }
        else
        {
            if(player.ammo <= 0 && !tutorial.partCompleted[3] && !control && GameObject.FindGameObjectsWithTag("Arrow").Length == 0)
            {

                tutorial.StartTutorialDialogue(12);
                tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1f;
                startTimer = true;
                waitTimer = 0;
                control = true;
                //guide.gameObject.GetComponent<Animator>().SetBool("Launch", true);
            }
        }

        if (startTimer)
        {
            waitTimer += Time.deltaTime;

            if(waitTimer > 1.8f && waitTimer < 1.9f)
                GameObject.FindGameObjectWithTag("NegroD'agua").GetComponent<Animator>().SetBool("Throw", true);

            if (waitTimer > 2)
            {
                GameObject extraArrow = (GameObject)Instantiate(extraArrows, points[0].transform.position, guide.gameObject.transform.rotation);
                extraArrow.GetComponent<AddArrow>().isTutorial = true;
                extraArrow.GetComponent<AddArrow>().point0 = points[0];
                extraArrow.GetComponent<AddArrow>().point1 = points[1];
                extraArrow.GetComponent<AddArrow>().point2 = points[2];
                extraArrow.GetComponent<AddArrow>().path = extraArrowsPoint.transform;
                GameObject.FindGameObjectWithTag("NegroD'agua").GetComponent<Animator>().SetBool("Throw", false);
                startTimer = false;
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
