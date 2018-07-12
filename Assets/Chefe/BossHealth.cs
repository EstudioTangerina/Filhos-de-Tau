using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {

    public int Maxhealth;
    public int Health;


	// Use this for initialization
	void Start () {
        Maxhealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(int dano)
    {
        Health -= dano;

    }
    }
