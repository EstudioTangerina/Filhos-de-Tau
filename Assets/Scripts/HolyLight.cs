using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLight : MonoBehaviour {
    public PlayerHealth player;
    public bool col;
	public Color color;
	public GameObject[] light;
	public Color color2;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
		color = GetComponent<SpriteRenderer>().color;
		color2 = light[0].GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		if(color.a <= 0)
		{
			GetComponent<SpriteRenderer>().color = color;
			light[0].GetComponent<SpriteRenderer>().color = color;
			light[1].GetComponent<SpriteRenderer>().color = color;
			Destroy(gameObject);
		}
		
		if(player.curHealth < player.maxHealth && col)
        {
            player.curHealth += 0.35f;
			color.a -= 0.002f;
			color2.a -= 0.001f;
			GetComponent<SpriteRenderer>().color = color;
			light[0].GetComponent<SpriteRenderer>().color = color2;
			light[1].GetComponent<SpriteRenderer>().color = color2;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            col = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            col = false;
    }
}
