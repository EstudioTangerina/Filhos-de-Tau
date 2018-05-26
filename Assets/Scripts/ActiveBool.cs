using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBool : MonoBehaviour {
    public TutorialManager tutorial;
    public bool canAttack;
    public bool changeWeapon;
    public bool change;
	// Use this for initialization
	void Start () {
        tutorial = FindObjectOfType<TutorialManager>();
	}
	
	// Update is called once per frame
	public void Change () {
            tutorial.canAttack = canAttack;
            tutorial.canChangeWeapon = changeWeapon;
            Destroy(gameObject);
    }
}
