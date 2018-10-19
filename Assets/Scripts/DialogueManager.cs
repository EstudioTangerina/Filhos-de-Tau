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
    public int index;
    public GameObject[] faces;
    public string[] facesNames;
    public bool speak, smile;
    private TutorialManager tutorial;
    bool firstDialogue;
    public bool lastDialogue;
    public AudioSource speakSound;
	// Use this for initialization
	void Start () {
        tutorial = GetComponent<TutorialManager>();
        tutorialBox.SetActive(false);
        firstDialogue = true;
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject go in faces)
        {
            if (go.activeSelf)
            {
                go.GetComponent<Animator>().SetBool("Smile", smile);
                go.GetComponent<Animator>().SetBool("Speak", speak);
            }
        }

        if (textBox.activeSelf == false)
            speakSound.Stop();

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
        speak = true;
        speakSound.Play();
        foreach(string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        speak = true;
        speakSound.Play();
        if (index == 0 || index == 2)
            nameText.text = "???";

        else if (index == 1 || index == 4 || index == 7)
            nameText.text = "Menina";

        else
            nameText.text = "Negro D'Água";

        for(int i = 0; i < facesNames.Length; i++)
        {
            if (facesNames[i] == nameText.text)
            {
                faces[i].SetActive(true);
            }

            else
                faces[i].SetActive(false);
        }

        if (sentences.Count == 0)
        {
            EndDialogue();

            foreach (GameObject go in faces)
                go.SetActive(false);

            return;
        }
        index++;
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

            if (dialogueText.text == sentence)
            {
                speak = false;
                speakSound.Stop();
            }
            yield return null;
        }
    }

    void EndDialogue()
    {
        if (!bossDialogue && !lastDialogue)
        {
            if(firstDialogue)
            {
                StartTutorialDialogue(tutorial.tutorialDialogues[13]);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IntructionButton(0);
                firstDialogue = false;
            }
            speakSound.Stop();
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

        else if(bossDialogue)
        {
            bossDialogueFinished = true;
            bossDialogue = false;
        }

        else if(lastDialogue)
        {
            textBox.SetActive(false);
            tutorial.FinishTutorial();
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
