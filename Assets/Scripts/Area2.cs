using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2 : MonoBehaviour {
    public RagDool[] ragdolls;
    public GameObject[] ragdollsShadow;
    public string[] colors;
    public int index;
    public List<int> validChoices = new List<int>();
    public int[] order;
    private GuideAi guide;
    public TutorialManager tutorial;
    public AreaReached area2;
    private bool actived;
	// Use this for initialization
	void Start () {
        actived = false;
        guide = FindObjectOfType<GuideAi>();

        for (int i = 0; i < order.Length; i++)
        {
            int x = Random.Range(0, validChoices.Count);
            order[i] = validChoices[x];
            validChoices.RemoveAt(x);
        }
        tutorial.tutorialDialogues[2].senteces[0] = "Agora bata nos bonecos na seguinte ordem " + colors[order[0]] + ", " + colors[order[1]] + ", " + colors[order[2]] + ", " + colors[order[3]] + ".";
	}
	
	// Update is called once per frame
	void Update () {
        if(guide.stoppedArea == 2 && area2.reached)
        {
            tutorial.StartTutorialDialogue(2);
            StartCoroutine("BlinkInOrder");
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 3;
            actived = true;
            area2.reached = false;
        }

        if (actived)
        {
            if (index != ragdolls.Length)
            {
                foreach (RagDool r in ragdolls)
                {
                    if (r.attack)
                    {
                        index++;
                        r.attack = false;
                        r.controle = true;
                    }
                }
            }

            if (index >= 0 && index != ragdolls.Length)
            {
                if (!ragdolls[order[index]].controle)
                {
                    index = -1;
                    tutorial.tutorialDialogues[2].senteces[0] = " Essa não foi a ordem que eu disse, tenta isso ai de novo, a ordem é essa: " + colors[order[0]] + ", " + colors[order[1]] + ", " + colors[order[2]] + ", " + colors[order[3]] + ".";
                    tutorial.StartTutorialDialogue(2);
                    StopAllCoroutines();

                    foreach (RagDool r in ragdolls)
                    {
                        r.controle = false;
                        r.attack = false;
                    }

                    StartCoroutine("BlinkInOrder");
                }
            }

            if (index >= ragdolls.Length - 1 && !tutorial.partCompleted[2])
            {
                tutorial.partCompleted[2] = true;
                tutorial.StartTutorialDialogue(3);
                GameObject.FindObjectOfType<GuideAi>().ChangeWaypoints(4);
                tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 0.8f;
            }
        }

        else
        {
            foreach (RagDool r in ragdolls)
            {
                r.controle = false;
                r.attack = false;
            }
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tutorial.partCompleted[2])
        {
            guide.ChangeWaypoints(2);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator BlinkInOrder()
    {
        ragdollsShadow[order[0]].SetActive(true);
        ragdollsShadow[order[1]].SetActive(false);
        ragdollsShadow[order[2]].SetActive(false);
        ragdollsShadow[order[3]].SetActive(false);

        yield return new WaitForSeconds(1.3f);

        ragdollsShadow[order[0]].SetActive(false);
        ragdollsShadow[order[1]].SetActive(true);
        ragdollsShadow[order[2]].SetActive(false);
        ragdollsShadow[order[3]].SetActive(false);

        yield return new WaitForSeconds(1.3f);

        ragdollsShadow[order[0]].SetActive(false);
        ragdollsShadow[order[1]].SetActive(false);
        ragdollsShadow[order[2]].SetActive(true);
        ragdollsShadow[order[3]].SetActive(false);


        yield return new WaitForSeconds(1.3f);

        ragdollsShadow[order[0]].SetActive(false);
        ragdollsShadow[order[2]].SetActive(false);
        ragdollsShadow[order[1]].SetActive(false);
        ragdollsShadow[order[3]].SetActive(true);

        yield return new WaitForSeconds(1.3f);

        ragdollsShadow[order[0]].SetActive(false);
        ragdollsShadow[order[1]].SetActive(false);
        ragdollsShadow[order[2]].SetActive(false);
        ragdollsShadow[order[3]].SetActive(false);

        yield return new WaitForSeconds(2.5f);
    }
}
