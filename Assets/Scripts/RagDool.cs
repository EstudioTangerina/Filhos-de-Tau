using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDool : MonoBehaviour {
    private EnemyHealth health;
    private bool attacked;
    private float timer;

    [SerializeField]
    AnimationClip attackedClip;

    public bool attack;
    public bool controle;
    // Use this for initialization
    void Start () {
        health = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("Attacked", attacked);

        if (health.curHealth == 0)
            attacked = true;

        if (attacked)
            timer += Time.deltaTime;

        if(timer >= attackedClip.length)
        {
            health.curHealth = health.maxHealth;
            attacked = false;
            timer = 0;
        }
	}
}
