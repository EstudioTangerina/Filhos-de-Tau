using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    private Queue<string> sentences = new Queue<string>();
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI tutorialText;
    public GameObject textBox;
    public GameObject tutorialBox;
    public GameObject playerHud;
    private float timer;
    public bool startTimer;
    private bool finishWrite;
    public float waitTime;
    public bool area4Dialogue;
    public bool area4DialogueFinished;
    public bool bossDialogue;
    public bool bossDialogueFinished;
    private TutorialManager tutorial;
	// Use this for initialization
	void Start () {
        tutorial = GetComponent<TutorialManager>();
        tutorialBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (finishWrite)
        {
            startTimer = true;
        }

        if (startTimer)
        {
            timer += Time.deltaTime;

            if(timer > waitTime)
            {
                DisplayNextSentenceTutorial();
                finishWrite = false;
                startTimer = false;
                timer = 0;
            }
        }
	}

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        //GetComponent<TutorialManager>().canWalk = false;
        //GetComponent<TutorialManager>().canAttack = false;
        playerHud.SetActive(false);
        GameObject.FindObjectOfType<GuideAi>().startMove = false;
        sentences.Clear();

        foreach(string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        textBox.SetActive(true);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        if (!bossDialogue)
        {
            GetComponent<TutorialManager>().canWalk = true;
            //tutorial.canWalk = tutorial.lastCanWalk;
            tutorial.canRun = tutorial.lastCanRun;
            tutorial.canAttack = tutorial.lastCanAttack;
            tutorial.canRoll = tutorial.lastCanRoll;
            tutorial.canUseMagic = tutorial.lastCanUseMagic;
            tutorial.canPursuit = tutorial.lastCanPursuit;
            tutorial.canChangeWeapon = tutorial.lastCanChangeWeapon;
            tutorial.canOpenInv = tutorial.lastCanChangeWeapon;
            playerHud.SetActive(true);
        }

        else
        {
            bossDialogueFinished = true;
            bossDialogue = false;
        }

        textBox.SetActive(false);
        GameObject.FindObjectOfType<GuideAi>().startMove = true;
    }

    public void StartTutorialDialogue(Dialogue dialogue)
    {
        timer = 0;
        finishWrite = false;
        startTimer = false;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentenceTutorial();
    }

    public void DisplayNextSentenceTutorial()
    {
        if (sentences.Count == 0)
        {
            EndDialogueTutorial();
            return;
        }
        tutorialBox.SetActive(true);
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentenceTutorial(sentence));
    }

    IEnumerator TypeSentenceTutorial(string sentence)
    {
        tutorialText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            tutorialText.text += letter;
            yield return null;
        }

        if(tutorialText.text == sentence)
        {
            finishWrite = true;
        }
    }

    void EndDialogueTutorial()
    {
        if (area4Dialogue)
        {
            area4DialogueFinished = true;
            area4Dialogue = false;
        }

        tutorialBox.SetActive(false);
    }
}
