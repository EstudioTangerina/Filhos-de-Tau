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
    public List<Sprite> buttonsImages = new List<Sprite>();

    public MenuManager menu;

    public bool haveSave;
    // Use this for initialization0
    void Start()
    {
        menu = GameObject.FindObjectOfType<MenuManager>();

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
            menu.RefreshButtonNames();
        }

        if (menu != null)
        {
            buttons = menu.buttons;

            for(int s = 0; s < menu.buttonsImages.Length; s++)
            {
                buttonsImages[s] = menu.buttonsImages[s];
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Menu")
        {
            if (FindObjectOfType<TutorialManager>().pausePanel.activeSelf == false && Time.timeScale > 0)
            {
                Pause();
                GameObject.Find("ButtonConfirm").GetComponent<AudioSource>().Play();
            }
            else if(FindObjectOfType<TutorialManager>().pausePanel.activeSelf == true)
            {
                Resume();
                GameObject.Find("ButtonBack").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void Pause()
    {
        AudioListener.pause = true;
        Time.timeScale = 0;
        FindObjectOfType<TutorialManager>().pausePanel.SetActive(true);
    }

    public void Resume()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        FindObjectOfType<TutorialManager>().pausePanel.SetActive(false);
    }

    public void Restart()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        SaveState(false);
        player = null;
        haveSave = true;
        SceneManager.LoadScene("Menu");
    }

    public void SaveState(bool start)
    {
        save = new SaveGame();
        if (start == true) // Reset variables
        {
            player.GetComponent<PlayerMovement>().ammo = 0;
            player.transform.position = new Vector3(-48.95f, 30.2f, 0);
            GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-49.34f, 31.14f, -10);
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
    }

    public void LoadState()
    {
        string caminho = Path.Combine(Application.persistentDataPath, "savegame.dat");
        string texto = File.ReadAllText(caminho);
        save = JsonUtility.FromJson<SaveGame>(texto);
    }

    public void NewGame()
    {
        StartCoroutine(LoadAsynchronously(1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        menu.loadingBar.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            menu.load.fillAmount = progress;
            menu.percent.text = Mathf.RoundToInt(progress * 100).ToString() + "%";
            yield return null;
        }
    }
}