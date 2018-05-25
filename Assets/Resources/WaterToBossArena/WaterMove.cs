using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour {
    private Transform objective;
    private bool walkToObjective;
    public float vel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (walkToObjective)
        {
            float objectiveDist = Vector2.Distance(transform.position, objective.position);

            if (objectiveDist > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, objective.position, vel * Time.deltaTime);
            }

            else
                walkToObjective = false;
        }
    }

    public void FollowObjective(Transform target)
    {
        objective = target;
        walkToObjective = true;
    }
}
