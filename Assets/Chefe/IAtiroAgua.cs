using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAtiroAgua : MonoBehaviour {

    public int x;
    public int y;
    public int z;
    int velocidade;

    public GameObject Player;

    // Use this for initialization
    void Start () {
        velocidade = 1;

        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left + Vector3.up * velocidade * Time.deltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(15);
           
        }
    }
}
