using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReOrderLayer : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public float offset;
    public bool isPlayer;
    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sortingOrder = Mathf.FloorToInt((transform.position.y - offset) * -1);

        if(!isPlayer)
        {
            if (player.transform.position.y > transform.position.y)
                spriteRenderer.sortingLayerName = "Objects+";

            else if(player.transform.position.y < transform.position.y)
                spriteRenderer.sortingLayerName = "Objects-";
        }
    }
}
