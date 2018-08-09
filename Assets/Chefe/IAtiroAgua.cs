﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAtiroAgua : MonoBehaviour {

    public bool esquerda;
    public bool direita;
    public bool cima;
    public bool baixo;
    public float velocidade;

    public GameObject Player;

    // Use this for initialization
    void Start () {
        

        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        velocidade = 3;
        if (cima == true)
        {
            transform.Translate(Vector3.up * velocidade * Time.deltaTime);
        }
        if (esquerda == true)
        {
            transform.Translate(Vector3.left * velocidade * Time.deltaTime);
        }
        if (direita == true)
        {
            transform.Translate(Vector3.right * velocidade * Time.deltaTime);
        }
        if (baixo == true)
        {
            transform.Translate(Vector3.down * velocidade * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerHealth>().TakeDamage(15);
           
        }
    }
}
