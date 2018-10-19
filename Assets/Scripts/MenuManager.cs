using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour {
    private GameManager manager;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private AudioMixer musicMixer;

    [SerializeField]
    private TMP_Dropdown resolutionsDropDown;

    private Resolution[] resolutions;

    [SerializeField]
    private Toggle fullscreenToggle;

    private int iter = 0;

    public Image volumeState;
    public Image MusicState;

    public Sprite[] volState = new Sprite[4];
    public Sprite[] musState = new Sprite[4];

    public List<KeyCode> buttons = new List<KeyCode>();

    public Object[] allKeyImages;
    public Sprite[] buttonsImages;

    public List<KeyCode> defaultButtons = new List<KeyCode>();
    public Button[] keyButtons;
    public TextMeshProUGUI[] keyButtonsText;

    public bool changeActive;
    public int i;
    public GameObject buttonWarning;
    public float timer;

    public Button loadButton;

    public Slider volume;
    public Slider musicVol;

    public GameObject settingsMenu;

    public Button back, reset;

    public GameObject loadingBar;
    public Image load;
    public TextMeshProUGUI percent;
    // Use this for initialization
    private void Awake()
    {
        settingsMenu.SetActive(false);
    }

    void Start () {

        allKeyImages = Resources.LoadAll("Buttons/Keyboard & Mouse/Dark", typeof(Sprite));

        float vol;
        bool result = audioMixer.GetFloat("volume", out vol);

        if (result)
            volume.value = vol;

        else
            volume.value = volume.maxValue;


        float mus;
        bool res = musicMixer.GetFloat("MusicVolume", out mus);

        if (result)
            musicVol.value = mus;

        else
            musicVol.value = musicVol.maxValue;

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

        if (volume.value > -10)
            volumeState.sprite = volState[3];

        else if (volume.value > -50 && volume.value < -10)
            volumeState.sprite = volState[2];

        else if (volume.value > -80 && volume.value <= -50)
            volumeState.sprite = volState[1];

        else
            volumeState.sprite = volState[0];

        if (musicVol.value > -10)
            MusicState.sprite = musState[3];

        else if (musicVol.value > -50 && musicVol.value < -10)
            MusicState.sprite = musState[2];

        else if (musicVol.value > -80 && musicVol.value <= -50)
            MusicState.sprite = musState[1];

        else
            MusicState.sprite = musState[0];


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
                        if (buttons.Contains(key) == false && key != KeyCode.Escape || buttons[i] == key && key != KeyCode.Escape)
                        {
                            buttons[i] = key;
                            keyButtonsText[i].fontSize = 40;
                            keyButtonsText[i].text = key.ToString();

                            foreach(Sprite a in allKeyImages)
                            {
                                if(key.ToString() == a.name)
                                    buttonsImages[i] = a;
                            }

                            foreach (Button b in keyButtons)
                                b.interactable = true;

                            back.interactable = true;
                            reset.interactable = true;
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

            foreach (Sprite a in allKeyImages)
            {
                if (buttons[i].ToString() == a.name)
                    buttonsImages[i] = a;
            }
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

    public void SetVolume(float v)
    {
        audioMixer.SetFloat("volume", v);
    }

    public void SetMusicVolume(float v)
    {
       musicMixer.SetFloat("MusicVolume", v);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;

        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        var width = int.Parse(resolutionsDropDown.options[resolutionIndex].text.Split(' ')[0]);
        var height = int.Parse(resolutionsDropDown.options[resolutionIndex].text.Split(' ')[2]);

        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void OpenPage()
    {
        Application.OpenURL("https://www.facebook.com/navegames1/");
    }

    public void ChangeButton(int buttonIndex)
    {
        if (keyButtons[buttonIndex].interactable)
        {
            if (!changeActive)
            {
                foreach (Button b in keyButtons)
                    b.interactable = false;

                back.interactable = false;
                reset.interactable = false;
                i = buttonIndex;
                keyButtonsText[i].fontSize = 25;
                keyButtonsText[i].text = "Aperte algum botão";
                changeActive = true;
            }
        }
    }
}
