using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBool : MonoBehaviour {
    public TutorialManager tutorial;
    public bool canAttack;
    public bool changeWeapon;
    public bool change;
    public int weaponIndex;
    public AnimatorOverrideController controller;
    public GameObject mouseScroll;
	// Use this for initialization
	void Start () {
        tutorial = FindObjectOfType<TutorialManager>();
	}
	
	// Update is called once per frame
	public void Change () {
            if (mouseScroll != null)
                mouseScroll.SetActive(true);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IntructionButton(5);
            tutorial.canAttack = canAttack;
            tutorial.canChangeWeapon = changeWeapon;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().runtimeAnimatorController = controller;
            GameObject.FindGameObjectWithTag("Player").GetComponent<ItemHUD>().curWeapon = weaponIndex;
            Destroy(gameObject);
    }
}
