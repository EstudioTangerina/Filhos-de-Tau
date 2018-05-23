﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area2 : MonoBehaviour {
    public RagDool[] ragdolls;
    public string[] colors;
    public int index;
    public List<int> validChoices = new List<int>();
    public int[] order;
    private GuideAi guide;
    public TutorialManager tutorial;
	// Use this for initialization
	void Start () {
        guide = FindObjectOfType<GuideAi>();

        for (int i = 0; i < order.Length; i++)
        {
            int x = Random.Range(0, validChoices.Count);
            order[i] = validChoices[x];
            validChoices.RemoveAt(x);
        }
        tutorial.tutorialDialogues[2].senteces[0] = "Agora bata nos bonecos nessa ordem: " + colors[order[0]] + ", " + colors[order[1]] + ", " + colors[order[2]] + ", " + colors[order[3]] + ", " + colors[order[4]] + ".";
	}
	
	// Update is called once per frame
	void Update () {
        if (index != 5)
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
        if (index >= 0 && index != 5)
        {
            if (!ragdolls[order[index]].controle)
            {
                index = -1;
                tutorial.tutorialDialogues[2].senteces[0] = "Você errou, o correto é: " + colors[order[0]] + ", " + colors[order[1]] + ", " + colors[order[2]] + ", " + colors[order[3]] + ", " + colors[order[4]] + ".";
                tutorial.StartTutorialDialogue(2);
                foreach (RagDool r in ragdolls)
                {
                    r.controle = false;
                    r.attack = false;
                }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && tutorial.partCompleted[2])
        {
            guide.ChangeWaypoints(2);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
