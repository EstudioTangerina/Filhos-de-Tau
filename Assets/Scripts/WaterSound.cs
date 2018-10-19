using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSound : MonoBehaviour {
    private Transform player;
    public float dist;
    public float vol;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        dist = Vector2.Distance(transform.position, player.position);

        vol = 1 - (dist / 13);

        if (vol <= 0.3f && vol >= 0)
            GetComponent<AudioSource>().volume = vol;

        else if(vol > 0.3f)
            GetComponent<AudioSource>().volume = 0.3f;

        else
            GetComponent<AudioSource>().volume = 0;

        if (dist < 14 && !GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();

        else if (dist >= 14 && GetComponent<AudioSource>().isPlaying)
        {
                GetComponent<AudioSource>().Stop();
        }
	}
}
