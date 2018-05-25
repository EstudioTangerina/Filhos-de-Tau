using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TutorialManager : MonoBehaviour {
    private GameObject player;
    private InventoryUI inventoryUI;

    [Header("Tutorial Lock Variable Controllers")]
    public bool canWalk;
    public bool canRun;
    public bool canAttack;
    public bool canRoll;
    public bool canUseMagic;
    public bool canPursuit;
    public bool canChangeWeapon;
    public bool canOpenInv;

    [Space(10)]

    public bool startBossFight;
    public GameObject startBossFightCol;
    public Transform bossArenaMiddle;
    public GameObject bossArenaCol;
    public GameObject normalCol;
    public GameObject wallCol;
    public GameObject topGround;
    public GameObject hud;

    public int levelPart;
    public bool[] partCompleted;
    public Transform[] areaMiddle;
    public float range;
    public bool walkBack;

    public Dialogue[] dialogues;
    public Dialogue[] tutorialDialogues;
    private DialogueManager dManager;

    [HideInInspector]
    public bool lastCanWalk;
    [HideInInspector]
    public bool lastCanRun;
    [HideInInspector]
    public bool lastCanAttack;
    [HideInInspector]
    public bool lastCanRoll;
    [HideInInspector]
    public bool lastCanUseMagic;
    [HideInInspector]
    public bool lastCanPursuit;
    [HideInInspector]
    public bool lastCanChangeWeapon;
    [HideInInspector]
    public bool lastCanOpenInv;

    public GameObject fade;
    public GameObject water;
    public GameObject cam;
    private bool control;
    // Use this for initialization
    void Start () {
        dManager = GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        //StartDialogue(0);

        player.GetComponent<PlayerMovement>().canWalk = canWalk;
        player.GetComponent<PlayerMovement>().canRun = canRun;
        player.GetComponent<PlayerMovement>().canAttack = canAttack;
        player.GetComponent<PlayerMovement>().canRoll = canRoll;
        player.GetComponent<PlayerMovement>().canUseMagic = canUseMagic;
        player.GetComponent<PlayerMovement>().canPursuit = canPursuit;
        player.GetComponent<ItemHUD>().canChangeWeapon = canChangeWeapon;
        inventoryUI.canOpenInv = canOpenInv;
        fade.SetActive(true);
        water.SetActive(false);

        lastCanWalk = canWalk;
        lastCanRun = canRun;
        lastCanAttack = canAttack;
        lastCanRoll = canRoll;
        lastCanUseMagic = canUseMagic;
        lastCanPursuit = canUseMagic;
        lastCanChangeWeapon = canChangeWeapon;
        lastCanOpenInv = canOpenInv;
    }

    private void Update()
    {
        if(partCompleted[levelPart] == false)
        {
            if (!dManager.tutorialBox.activeSelf && !walkBack)
            {
                StartTutorialDialogue(4);
                dManager.waitTime = 0.8f;
            }

            FollowObjective(areaMiddle[levelPart - 1]);
            levelPart -= 1;
            walkBack = true;
        }

        if (walkBack)
        {
            range = Vector2.Distance(player.transform.position, areaMiddle[levelPart].position);

            if(range == 0)
            {
                canWalk = lastCanWalk;
                canRun = lastCanRun;
                canAttack = lastCanAttack;
                canRoll = lastCanRoll;
                canUseMagic = lastCanUseMagic;
                canPursuit = lastCanPursuit;
                canChangeWeapon = lastCanChangeWeapon;
                canOpenInv = lastCanChangeWeapon;
                hud.SetActive(true);
                walkBack = false;
            }

        }

        if (startBossFight)
        {
            float r = Vector2.Distance(player.transform.position, bossArenaMiddle.position);

            if (r < 10)
            {
                normalCol.SetActive(false);
                wallCol.GetComponent<CompositeCollider2D>().isTrigger = true;
                topGround.GetComponent<TilemapRenderer>().sortingLayerName = "Objects";
                topGround.GetComponent<TilemapRenderer>().sortingOrder = -100;
            }
                if (r == 0 && !bossArenaCol.activeSelf)
            {
                StartDialogue(1);
                bossArenaCol.SetActive(true);
            }

            if (player.GetComponent<PlayerMovement>().objectiveDist == 0 && !control)
            {
                player.GetComponent<PlayerMovement>().LookTo(0, 1);
                control = true;
            }

        }

        if(dManager.bossDialogueFinished)
        {
            float d = Vector2.Distance(water.transform.position, bossArenaMiddle.position);

            if(!water.activeSelf)
            {
                cam.GetComponent<SmoothCamera2D>().ShakeCamera(6, 0.1f);
                water.SetActive(true);
                water.GetComponent<WaterMove>().FollowObjective(bossArenaMiddle);
            }

            if(d == 0)
            {
                if (cam.gameObject.GetComponent<Camera>().orthographicSize < 7)
                {
                    cam.gameObject.GetComponent<Camera>().orthographicSize += 0.1f;
                }

                if (!hud.activeSelf && cam.gameObject.GetComponent<Camera>().orthographicSize >= 7)
                {
                    canWalk = lastCanWalk;
                    canRun = lastCanRun;
                    canAttack = lastCanAttack;
                    canRoll = lastCanRoll;
                    canUseMagic = lastCanUseMagic;
                    canPursuit = lastCanPursuit;
                    canChangeWeapon = lastCanChangeWeapon;
                    canOpenInv = lastCanChangeWeapon;
                    hud.SetActive(true);
                    dManager.bossDialogueFinished = false;
                }
            }

        }
    }

    // Update is called once per frame
    void LateUpdate () {
        player.GetComponent<PlayerMovement>().canWalk = canWalk;
        player.GetComponent<PlayerMovement>().canRun = canRun;
        player.GetComponent<PlayerMovement>().canAttack = canAttack;
        player.GetComponent<PlayerMovement>().canRoll = canRoll;
        player.GetComponent<PlayerMovement>().canUseMagic = canUseMagic;
        player.GetComponent<PlayerMovement>().canPursuit = canPursuit;
        player.GetComponent<ItemHUD>().canChangeWeapon = canChangeWeapon;
        inventoryUI.canOpenInv = canOpenInv;
    }

    public void StartBossFight()
    {
        lastCanWalk = canWalk;
        lastCanRun = canRun;
        lastCanAttack = canAttack;
        lastCanRoll = canRoll;
        lastCanUseMagic = canUseMagic;
        lastCanPursuit = canUseMagic;
        lastCanChangeWeapon = canChangeWeapon;
        lastCanOpenInv = canOpenInv;

        player.GetComponent<PlayerMovement>().FollowObjective(bossArenaMiddle);
        startBossFightCol.SetActive(false);
        startBossFight = true;
        canWalk = false;
        canRun = false;
        canAttack = false;
        canRoll = false;
        canUseMagic = false;
        canPursuit = false;
        hud.SetActive(false);
        canChangeWeapon = false;
    }

    public void FollowObjective(Transform t)
    {
        lastCanWalk = canWalk;
        lastCanRun = canRun;
        lastCanAttack = canAttack;
        lastCanRoll = canRoll;
        lastCanUseMagic = canUseMagic;
        lastCanPursuit = canUseMagic;
        lastCanChangeWeapon = canChangeWeapon;
        lastCanOpenInv = canOpenInv;

        player.GetComponent<PlayerMovement>().FollowObjective(t);
        canWalk = false;
        canRun = false;
        canAttack = false;
        canRoll = false;
        canUseMagic = false;
        canPursuit = false;
        hud.SetActive(false);
        canChangeWeapon = false;
    }

    public void StartDialogue(int i)
    {
        canWalk = false;
        canRun = false;
        canAttack = false;
        canRoll = false;
        canUseMagic = false;
        canPursuit = false;
        hud.SetActive(false);
        canChangeWeapon = false;

        if (i == 1)
            dManager.bossDialogue = true;

        dManager.StartDialogue(dialogues[i]);
    }

    public void StartTutorialDialogue(int i)
    {
        if (i == 11)
            dManager.area4Dialogue = true;

        dManager.StartTutorialDialogue(tutorialDialogues[i]);
    }
}
