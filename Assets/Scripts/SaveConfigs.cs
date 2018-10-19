using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveConfigs : MonoBehaviour {
    public string[] buttons = new string[10];

    // Use this for initialization
    void Start () {
        buttons[0] = "upButton";
        buttons[1] = "downButton";
        buttons[2] = "leftButton";
        buttons[3] = "rightButton";
        buttons[4] = "runButton";
        buttons[5] = "attackButton";
        buttons[6] = "pickUpButton";
        buttons[7] = "rollButton";
        buttons[8] = "invButton";
        buttons[9] = "specialButton";

        GetControls();

        GetComponent<MenuManager>().RefreshButtonNames();
        SetNewControls();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void GetControls()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if (PlayerPrefs.HasKey(buttons[i]))
            {
                GetComponent<MenuManager>().buttons[i] = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(buttons[i]));

                foreach(Sprite s in GetComponent<MenuManager>().allKeyImages)
                {
                    if (s.name == GetComponent<MenuManager>().buttons[i].ToString())
                        GetComponent<MenuManager>().buttonsImages[i] = s;
                }
            }
        }
    }

    public void SetNewControls()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            PlayerPrefs.SetString(buttons[i], GetComponent<MenuManager>().buttons[i].ToString());
        }
    }
}
