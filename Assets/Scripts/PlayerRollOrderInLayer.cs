using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollOrderInLayer : MonoBehaviour {
    private GameObject player;
    private ReOrderLayer layer;
    private SpriteRenderer render;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        layer = GetComponent<ReOrderLayer>();
        render = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<Animator>().GetBool("Roll"))
        {
            layer.enabled = false;
            render.sortingOrder = player.GetComponent<SpriteRenderer>().sortingOrder - 1;
        }

        else
            layer.enabled = true;
	}
}
