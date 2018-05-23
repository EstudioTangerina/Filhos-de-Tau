using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    public TutorialManager tutorial;
    public int index;
    private bool passed;
    public float wait = 1.5f;

    public void TriggerDialogue()
    {
        tutorial.StartTutorialDialogue(index);
        tutorial.gameObject.GetComponent<DialogueManager>().waitTime = wait;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !passed)
        {
            TriggerDialogue();
            passed = true;
        }
    }
}
