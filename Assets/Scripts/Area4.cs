using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area4 : MonoBehaviour {
    public int logsRolled;
    public TutorialManager tutorial;
    private bool one;
    private bool three;
    private bool startWave;
    private float timer;
    public GameObject[] enemies;
    private Transform player;
    public SmoothCamera2D cam;
    public Transform mid;
    public float timer2;
    public bool zoomIn;
    public bool zoomOut;
    public bool zoomOutMid;
    public GameObject guide;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
       if(logsRolled == 1 && !one)
        {
            tutorial.StartTutorialDialogue(8);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1f;
            one = true;
        }

        else if (logsRolled == 3 && !three)
        {
            tutorial.StartTutorialDialogue(9);
            //tutorial.partCompleted[4] = true;
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1f;
            three = true;
        }

        if (tutorial.GetComponent<DialogueManager>().area4DialogueFinished)
        {
            guide.GetComponent<GuideAi>().ChangeWaypoints(5);
            guide.GetComponent<GuideAi>().vel *= 1.5f;
            guide.GetComponent<Animator>().speed = 1.3f;
            tutorial.GetComponent<DialogueManager>().area4DialogueFinished = false;
        }

        if (GameObject.FindObjectOfType<EnemyAI>() == null && startWave && !tutorial.partCompleted[4])
        {
            tutorial.StartTutorialDialogue(11);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 1f;
            tutorial.partCompleted[4] = true;
        }

        if (three && !startWave)
            timer += Time.deltaTime;

        if (startWave && timer2 < 25)
            timer2 += Time.deltaTime;

        #region Zoom out to face arena
        if (timer2 > 2.5f && timer2 < 2.6f)
        {
            cam.target = mid.transform;
            zoomOutMid = true;
        }

        if (zoomOutMid && cam.gameObject.GetComponent<Camera>().orthographicSize < 10)
        {
            cam.gameObject.GetComponent<Camera>().orthographicSize += 0.1f;
        }

        else if (zoomOutMid && cam.gameObject.GetComponent<Camera>().orthographicSize >= 10)
            zoomOutMid = false;
        #endregion

        #region Zoom in to face rhythm bar
        if (timer2 > 8.5 && timer2 < 8.6f)
        {
            cam.target = enemies[0].transform;
            zoomIn = true;
        }

        if (zoomIn && cam.gameObject.GetComponent<Camera>().orthographicSize > 1.75)
        {
            cam.gameObject.GetComponent<Camera>().orthographicSize -= 0.1f;
        }

        else if (zoomIn && cam.gameObject.GetComponent<Camera>().orthographicSize <= 1.75)
            zoomIn = false;
        #endregion

        #region Zoom out to face player
        if (timer2 > 21 && timer2 < 21.1f)
        {
            cam.target = player;
            zoomOut = true;
        }

        if (zoomOut && cam.gameObject.GetComponent<Camera>().orthographicSize < 5)
        {
            cam.gameObject.GetComponent<Camera>().orthographicSize += 0.1f;
        }

        else if (zoomOut && cam.gameObject.GetComponent<Camera>().orthographicSize >= 5)
        {
            tutorial.canWalk = tutorial.lastCanWalk;
            tutorial.canRun = tutorial.lastCanRun;
            tutorial.canAttack = tutorial.lastCanAttack;
            tutorial.canRoll = tutorial.lastCanRoll;
            tutorial.canUseMagic = tutorial.lastCanUseMagic;
            tutorial.canUseMagic = tutorial.lastCanPursuit;
            tutorial.canChangeWeapon = tutorial.lastCanChangeWeapon;
            tutorial.canOpenInv = tutorial.lastCanOpenInv;
            tutorial.hud.SetActive(true);

            for(int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyAI>().canWalk = true;
                enemies[i] = null;
            }
            zoomOut = false;
        }
        #endregion

        if (timer > 3f && !tutorial.gameObject.GetComponent<DialogueManager>().tutorialBox.activeSelf && !startWave)
        {
            foreach (GameObject go in enemies)
            {
                go.GetComponent<EnemyAI>().canWalk = false;
                go.SetActive(true);
            }

            tutorial.StartTutorialDialogue(10);
            tutorial.gameObject.GetComponent<DialogueManager>().waitTime = 2f;
            tutorial.lastCanWalk = tutorial.canWalk;
            tutorial.lastCanRun = tutorial.canRun;
            tutorial.lastCanAttack = tutorial.canAttack;
            tutorial.lastCanRoll = tutorial.canRoll;
            tutorial.lastCanUseMagic = tutorial.canUseMagic;
            tutorial.lastCanPursuit = tutorial.canUseMagic;
            tutorial.lastCanChangeWeapon = tutorial.canChangeWeapon;
            tutorial.lastCanOpenInv = tutorial.canOpenInv;
            tutorial.hud.SetActive(false);

            tutorial.canWalk = false;
            tutorial.canRun = false;
            tutorial.canAttack = false;
            tutorial.canRoll = false;
            tutorial.canUseMagic = false;
            tutorial.canUseMagic = false;
            tutorial.canChangeWeapon = false;
            tutorial.canOpenInv = false;
            player.GetComponent<PlayerMovement>().isWalking = false;

            startWave = true;
        }
    }
}
