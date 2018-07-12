using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLife : MonoBehaviour {
    private PlayerHealth player;
    public int amount;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Change()
    {
        player.curHealth += amount;
        Destroy(gameObject);
    }
}
