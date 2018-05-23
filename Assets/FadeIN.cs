using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIN : MonoBehaviour {
    public Sprite[] states;
    public float timer;
    public int i;
    private Image img;
	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (i < states.Length)
        {
            timer += Time.deltaTime;
            img.sprite = states[i];
            if (timer > 0.003f)
            {
                i++;
                timer = 0;
            }

        }

        else
            this.gameObject.SetActive(false);
	}
}
