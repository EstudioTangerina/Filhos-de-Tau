using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAI : MonoBehaviour {
    private bool isFurious;
    private EnemyHealth health;
    private EnemyAI ai;
    private Animator anim;

    private bool controle;

    [SerializeField]
    protected GameObject clawsPrefab;

    [SerializeField]
    private AnimationClip furiousClip;

    private float timer;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        ai = GetComponent<EnemyAI>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health.curHealth <= health.maxHealth / 2)
            Furious();

        anim.SetBool("isFurious", isFurious);
	}

    void Furious()
    {
        if (ai.dmg != 20)
            isFurious = true;

        if (ai.isAttacking)
            controle = true;

        ai.dmg = 20;
        ai.speed = 4.5f;

        timer += Time.deltaTime;

        if(timer >= furiousClip.length)
        {
            isFurious = false;
            timer = 0;
        }

        if (ai.controle && ai.isAttacking == false && controle && Random.Range(0, 30) == 5 && anim.GetBool("Died") == false && anim.GetBool("Hurt") == false && isFurious == false)
        { 
            Vector3 pos = new Vector3();

            if (anim.GetFloat("x") < 0 && anim.GetFloat("y") == 0)
                pos = new Vector3(transform.position.x - 1.5f, transform.position.y + 0.06313944f, transform.position.z);

            else if (anim.GetFloat("x") > 0 && anim.GetFloat("y") == 0)
                pos = new Vector3(transform.position.x + 1.5f, transform.position.y + 0.06313944f, transform.position.z);

            else if (anim.GetFloat("x") == 0 && anim.GetFloat("y") > 0)
                pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);

            else if (anim.GetFloat("x") == 0 && anim.GetFloat("y") < 0)
                pos = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);


            GameObject claw = (GameObject)Instantiate(clawsPrefab, pos, transform.rotation);
            Destroy(claw, 1);

            controle = false;
        }
    }
}
