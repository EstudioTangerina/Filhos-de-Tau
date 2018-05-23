using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class SaveGame {
    //public int playerAmmo;
    public Vector3 playerPos;
    public Vector3 camPos;
    public int ammo;
    public float playerHealth;
    public float playerEnergy;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
}
