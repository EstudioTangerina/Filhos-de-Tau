using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ItemHUD : MonoBehaviour {
    [HideInInspector]
    public bool canChangeWeapon;

    [SerializeField]
    private GameObject itemBox;

    [SerializeField]
    private List<Sprite> itemImg = new List<Sprite>();

    [SerializeField]
    private TextMeshProUGUI itenAmount;

    [SerializeField]
    private GameObject[] curItem;

    private PlayerMovement plMove;

    public int curWeapon;

    [SerializeField]
    private RuntimeAnimatorController[] animStates;

    private bool controle;
    // Use this for initialization
    void Start () {
        plMove = GetComponent<PlayerMovement>();
	}

    private void LateUpdate()
    {
        if (itemImg.Count > 0)
        {
            plMove.activeWeapon = itemImg[curWeapon].name;

            for (int i = 0; i < curItem.Length; i++)
            {
                if (curWeapon == i)
                {
                    curItem[i].SetActive(true);
                }

                else
                    curItem[i].SetActive(false);
            }  
        }
    }

    // Update is called once per frame
    void Update () {
        itenAmount.text = "x" + plMove.ammo;

        if (FindObjectOfType<TutorialManager>().canAttack)
        {
            itemBox.SetActive(true);

            if (canChangeWeapon && Time.timeScale > 0)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0 )
                {
                    if (curWeapon < curItem.Length - 1)
                        curWeapon += 1;

                    else
                        curWeapon = 0;

                    GetComponent<Animator>().runtimeAnimatorController = animStates[curWeapon];
                }

                else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if (curWeapon > 0)
                        curWeapon -= 1;

                    else
                        curWeapon = curItem.Length - 1;
                    GetComponent<Animator>().runtimeAnimatorController = animStates[curWeapon];
                }
            }
        }

        else
            itemBox.SetActive(false);
    }
}
