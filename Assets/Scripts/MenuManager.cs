using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    private GameManager manager;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Dropdown resolutionsDropDown;

    private Resolution[] resolutions;

    [SerializeField]
    private Toggle fullscreenToggle;

    private int iter = 0;

    public List<KeyCode> buttons = new List<KeyCode>();
    public List<KeyCode> defaultButtons = new List<KeyCode>();
    public Button[] keyButtons;
    public Text[] keyButtonsText;

    public bool changeActive;
    public int i;
    public GameObject buttonWarning;
    public float timer;

    public Button loadButton;
    // Use this for initialization
    void Start () {
        buttonWarning.SetActive(false);

        if (Screen.fullScreen)
            fullscreenToggle.isOn = true;

        else
            fullscreenToggle.isOn = false;

        resolutions = Screen.resolutions;

        resolutionsDropDown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;

            if (options.Contains(option.ToLower()))
                continue;

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = iter;

            iter++;
        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = currentResolutionIndex;
        resolutionsDropDown.RefreshShownValue();
	}
	
	// Update is called once per frame
	void Update () {
        if (manager == null)
            manager = GameObject.Find("MenuManager").GetComponent<GameManager>();

        loadButton.interactable = manager.haveSave;
        
        #region Change Controller
        if (changeActive)
        {
            if (timer < 0.4f)
                timer += Time.deltaTime;

            if (timer > 0.3f)
            {
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyUp(key))
                    {
                        if (buttons.Contains(key) == false || buttons[i] == key)
                        {
                            buttons[i] = key;
                            keyButtonsText[i].text = key.ToString();
                            foreach (Button b in keyButtons)
                                b.interactable = true;
                            changeActive = false;
                            timer = 0;
                            GetComponent<SaveConfigs>().SetNewControls();
                            buttonWarning.SetActive(false);
                        }

                        else
                            buttonWarning.SetActive(true);
                    }
                }
            }
        }
        #endregion
    }
    public void RefreshButtonNames()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            keyButtonsText[i].text = buttons[i].ToString();
        }
    }

    public void ResetControls()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i] = defaultButtons[i];
        }
        RefreshButtonNames();
        GetComponent<SaveConfigs>().SetNewControls();
    }

    public void LoadSave()
    {
        manager.LoadGame();
    }

    public void NewSave()
    {
        manager.NewGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        var width = int.Parse(resolutionsDropDown.options[resolutionIndex].text.Split(' ')[0]);
        var height = int.Parse(resolutionsDropDown.options[resolutionIndex].text.Split(' ')[2]);

        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void ChangeButton(int buttonIndex)
    {
        if (keyButtons[buttonIndex].interactable)
        {
            if (!changeActive)
            {
                foreach (Button b in keyButtons)
                    b.interactable = false;

                i = buttonIndex;
                keyButtonsText[i].text = "Press any button";
                changeActive = true;
            }
        }
    }
}
