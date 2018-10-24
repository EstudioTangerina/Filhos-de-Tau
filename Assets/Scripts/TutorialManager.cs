using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

public class TutorialManager : MonoBehaviour {
    private GameObject player;
    public GameObject negroDagua;
    private InventoryUI inventoryUI;
    public AudioMixer audioMixer;
    public AudioMixer musicMixer;
    public Slider volume;
    public Slider volumeMusic;
    public Image volumeState;
    public Image MusicState;
    public Sprite[] volState = new Sprite[4];
    public Sprite[] musState = new Sprite[4];
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
    public GameObject ramp, rampTop;
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
    private GameManager manager;
    public GameObject pausePanel;
    public GameObject winPanel;

    public GameObject playerShaddow;

    public GameObject bossHealthBar;

    public GameObject[] rocks;
    public GameObject[] rocksShadows;

    public GameObject[] arrowAndBow;
    public ArrowTarget[] targets;
    public GameObject[] areas;

    public Vector3[] playerPos;
    public Vector3[] guidePos;

    public GameObject[] huds;

    public bool run1Time;
    // Use this for initialization
    void Start()
    {
        dManager = GetComponent<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        //StartDialogue(0);
        float vol;
        bool result = audioMixer.GetFloat("volume", out vol);

        if (result)
            volume.value = vol;

        else
            volume.value = volume.maxValue;

        float musVol;
        bool res = musicMixer.GetFloat("MusicVolume", out musVol);

        if (res)
            volumeMusic.value = musVol;

        else
            volumeMusic.value = volumeMusic.maxValue;

        if (GameObject.Find("MenuManager") != null)
        {
            manager = GameObject.Find("MenuManager").GetComponent<GameManager>();
            levelPart = manager.areaReached;
        }
        
        else
        {
            player.GetComponent<PlayerMovement>().canWalk = canWalk;
            player.GetComponent<PlayerMovement>().canRun = canRun;
            player.GetComponent<PlayerMovement>().canAttack = canAttack;
            player.GetComponent<PlayerMovement>().canRoll = canRoll;
            player.GetComponent<PlayerMovement>().canUseMagic = canUseMagic;
            player.GetComponent<PlayerMovement>().canPursuit = canPursuit;
            player.GetComponent<ItemHUD>().canChangeWeapon = canChangeWeapon;
        }

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
        if (GameObject.Find("MenuManager") != null)
        {
            manager = GameObject.Find("MenuManager").GetComponent<GameManager>();

            if (run1Time == false)
            {
                levelPart = manager.areaReached;

                if (manager.areaReached == 3)
                {/*
                canWalk = true;
                canRun = true;
                canAttack = true;*/
                    canRoll = false;
                    canUseMagic = false;
                    canPursuit = true;
                    canChangeWeapon = true;
                    canOpenInv = false;
                    player.GetComponent<PlayerMovement>().onStart = false;
                    player.GetComponent<PlayerMovement>().onStart2 = true;
                    hud.SetActive(true);
                    arrowAndBow[0].GetComponent<AddArrow>().Change();
                    arrowAndBow[1].GetComponent<ActiveBool>().Change();
                    partCompleted[0] = partCompleted[1] = partCompleted[2] = partCompleted[3] = true;
                    foreach (ArrowTarget a in targets)
                        a.Hit();

                    areas[0].SetActive(false);
                    areas[1].SetActive(false);

                    huds[0].SetActive(false);
                    huds[1].SetActive(false);

                    player.transform.position = playerPos[0];
                    negroDagua.transform.position = guidePos[0];
                    negroDagua.GetComponent<GuideAi>().stoppedArea = 4;
                    Camera.main.gameObject.transform.position = new Vector3(playerPos[0].x, playerPos[0].y, -10f);
                }

                else if (manager.areaReached == 4)
                {/*
                canWalk = true;
                canRun = true;
                canAttack = true;*/
                    canRoll = true;
                    canUseMagic = false;
                    canPursuit = true;
                    canChangeWeapon = true;
                    canOpenInv = false;
                    player.GetComponent<PlayerMovement>().onStart = false;
                    player.GetComponent<PlayerMovement>().onStart2 = true;
                    hud.SetActive(true);
                    arrowAndBow[0].GetComponent<AddArrow>().Change();
                    arrowAndBow[1].GetComponent<ActiveBool>().Change();
                    partCompleted[0] = partCompleted[1] = partCompleted[2] = partCompleted[3] = partCompleted[4] = true;
                    foreach (ArrowTarget a in targets)
                        a.Hit();

                    foreach (GameObject g in areas)
                        g.SetActive(false);

                    huds[0].SetActive(false);
                    huds[1].SetActive(false);

                    player.transform.position = playerPos[1];
                    negroDagua.transform.position = guidePos[1];
                    negroDagua.GetComponent<GuideAi>().stoppedArea = 5;
                    Camera.main.gameObject.transform.position = new Vector3(playerPos[1].x, playerPos[1].y, -10f);
                }

                else
                {
                    player.GetComponent<PlayerMovement>().canWalk = canWalk;
                    player.GetComponent<PlayerMovement>().canRun = canRun;
                    player.GetComponent<PlayerMovement>().canAttack = canAttack;
                    player.GetComponent<PlayerMovement>().canRoll = canRoll;
                    player.GetComponent<PlayerMovement>().canUseMagic = canUseMagic;
                    player.GetComponent<PlayerMovement>().canPursuit = canPursuit;
                    player.GetComponent<ItemHUD>().canChangeWeapon = canChangeWeapon;
                }

                run1Time = true;
            }
        }

        if (volume.value > -10)
            volumeState.sprite = volState[3];

        else if (volume.value > -50 && volume.value < -10)
            volumeState.sprite = volState[2];

        else if(volume.value > -80 && volume.value <= - 50)
            volumeState.sprite = volState[1];

        else
            volumeState.sprite = volState[0];

        if (volumeMusic.value > -10)
            MusicState.sprite = musState[3];

        else if (volumeMusic.value > -50 && volumeMusic.value < -10)
            MusicState.sprite = musState[2];

        else if (volumeMusic.value > -80 && volumeMusic.value <= -50)
            MusicState.sprite = musState[1];

        else
            MusicState.sprite = musState[0];

        if (partCompleted[levelPart] == false)
        {
            if (!dManager.tutorialBox.activeSelf && !walkBack)
            {
                StartTutorialDialogue(4);
                dManager.waitTime = 0.8f;
            }

            if(levelPart - 1 != 3)
                FollowObjective(areaMiddle[levelPart - 1]);

            levelPart -= 1;
            walkBack = true;
        }

        if (walkBack && levelPart - 1 != 3)
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
            manager.areaReached = 4;

            if (r < 13)
            {
                normalCol.SetActive(false);
                playerShaddow.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                playerShaddow.GetComponent<SpriteRenderer>().sortingOrder = -98;
                wallCol.GetComponent<CompositeCollider2D>().isTrigger = true;
                topGround.GetComponent<TilemapRenderer>().sortingLayerName = "Objects";
                topGround.GetComponent<TilemapRenderer>().sortingOrder = -100;
                rampTop.GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                rampTop.GetComponent<SpriteRenderer>().sortingOrder = -101;
                ramp.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                ramp.GetComponent<SpriteRenderer>().sortingOrder = 0;
                for (int i = 0; i < rocks.Length; i++)
                {
                    rocks[i].GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                    rocks[i].GetComponent<SpriteRenderer>().sortingOrder = -90;
                    rocksShadows[i].GetComponent<SpriteRenderer>().sortingLayerName = "Objects";
                    rocksShadows[i].GetComponent<SpriteRenderer>().sortingOrder = -98;
                }
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
                    FindObjectOfType<GuideBoss>().startMove = true;
                    bossHealthBar.SetActive(true);
                   /* winPanel.SetActive(true);
                    Time.timeScale = 0;*/
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
        /*
        if (manager != null && pausePanel.activeSelf == false)
            manager.areaReached = levelPart;*/
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

    public void Pause()
    { 
       if (manager != null && FindObjectOfType<GuideBoss>().beated == false) 
            manager.Pause();
    }

    public void Resume()
    {
        if (manager != null)
            manager.Resume();
    }

    public void Restart()
    {
        if (manager != null)
            manager.Restart();
    }

    public void MainMenu()
    {
        if (manager != null)
        {
            manager.areaReached = 0;
            manager.MainMenu();
        }
    }

    public void SetVolume(float v)
    {
        audioMixer.SetFloat("volume", v);
    }

    public void SetMusicVolume(float v)
    {
        musicMixer.SetFloat("MusicVolume", v);
    }

    public void FinishTutorial()
    {
        Debug.Log("Tutorial Finished");
        winPanel.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;
    }
}
