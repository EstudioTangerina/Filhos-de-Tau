using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSequence : MonoBehaviour {
    private DialogueManager dialogue;
	// Use this for initialization
	void Start () {
        dialogue = FindObjectOfType<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
            dialogue.DisplayNextSentence();
	}
}
