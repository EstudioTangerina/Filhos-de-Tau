using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string objetosalvo;
    public string caminho;
    public GameObject player;

    public SaveGame save;

    public List<KeyCode> buttons = new List<KeyCode>();
    public MenuManager menu;

    public bool haveSave;
    // Use this for initialization
    void Start()
    {
        menu = GameObject.FindObjectOfType<MenuManager>();
        //menu.buttons = buttons;

        if (GameObject.FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);

        else
            DontDestroyOnLoad(this.gameObject);

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        else
        {
            player = null;
        }

        if (System.IO.File.Exists(Path.Combine(Application.persistentDataPath, "savegame.dat")))
        {
            LoadState();
            haveSave = true;
        }

        else
            haveSave = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameObject.FindGameObjectWithTag("Player") != null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (menu == null && SceneManager.GetActiveScene().name == "Menu")
        {
            menu = GameObject.FindObjectOfType<MenuManager>();
            //menu.buttons = buttons;
            menu.RefreshButtonNames();
        }

        if (menu != null)
            buttons = menu.buttons;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveState(false);
            player = null;
            haveSave = true;
            SceneManager.LoadScene("Menu");
        }
        /*
        if (Input.GetKeyDown(KeyCode.R) && player != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }
    public void SaveState(bool start)
    {
        save = new SaveGame();
        if (start == true) // Reset variables
        {
            player.GetComponent<PlayerMovement>().ammo = 0;
            player.transform.position = new Vector3(-49.8f, 30.5f, 0);
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-49.8f, 30.5f, -10);
            player.GetComponent<PlayerHealth>().curHealth = player.GetComponent<PlayerHealth>().maxHealth;
            player.GetComponent<EnergyBar>().curEnergy = player.GetComponent<EnergyBar>().maxEnergy;
        }
        save.ammo = player.GetComponent<PlayerMovement>().ammo;
        save.playerPos = player.transform.position;
        save.camPos = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
        save.playerHealth = player.GetComponent<PlayerHealth>().curHealth;
        save.playerEnergy = player.GetComponent<EnergyBar>().curEnergy;

        string caminho = Path.Combine(Application.persistentDataPath, "savegame.dat");
        string objetosalvo = JsonUtility.ToJson(save, true);
        File.WriteAllText(caminho, objetosalvo);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
        StartCoroutine(WaitLoadScene(false));
    }

    public void LoadState()
    {
        string caminho = Path.Combine(Application.persistentDataPath, "savegame.dat");
        string texto = File.ReadAllText(caminho);
        save = JsonUtility.FromJson<SaveGame>(texto);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
        StartCoroutine(WaitLoadScene(true));
    }

    IEnumerator WaitLoadScene(bool newG)
    {
        AsyncOperation asyncLoadLevel;
        asyncLoadLevel = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        if (newG == true) // Reset
            SaveState(true);

        if (newG == false) // Load
        {
            player.GetComponent<PlayerMovement>().ammo = save.ammo;
            player.transform.position = save.playerPos;
            player.GetComponent<PlayerHealth>().curHealth = save.playerHealth;
            player.GetComponent<EnergyBar>().curEnergy = save.playerEnergy;
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = save.camPos;
        }
    }
}