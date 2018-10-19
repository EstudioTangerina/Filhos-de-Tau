using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour {
    private Transform objective;
    private bool walkToObjective;
    public float vel;
    public bool low;
    public AudioSource waterSong;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (walkToObjective)
        {
            float objectiveDist = Vector2.Distance(transform.position, objective.position);
            if(objectiveDist < 10 && low == false)
                low = true;

            if (objectiveDist > 0)
            {
                if (GetComponent<AudioSource>().volume < 0.3f && objectiveDist >= 10)
                    GetComponent<AudioSource>().volume += 0.002f;

                transform.position = Vector2.MoveTowards(transform.position, objective.position, vel * Time.deltaTime);
            }

            else
            {
                walkToObjective = false;
            }
        }

        if(low)
        {
            if (GetComponent<AudioSource>().volume > 0)
            {
                GetComponent<AudioSource>().volume -= 0.01f;

                if (GetComponent<AudioSource>().volume < 0.15f && waterSong.isPlaying == false)
                        waterSong.Play();
            }

            else
            {
                GetComponent<AudioSource>().Stop();

                low = false;
            }

        }
    }

    public void FollowObjective(Transform target)
    {
        objective = target;
        walkToObjective = true;
    }
}
