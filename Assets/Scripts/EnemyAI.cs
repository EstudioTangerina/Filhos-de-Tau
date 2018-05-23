using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Animator anim;
    public float maxDist;
    public float minDist;
    public bool isAttacking;
    public bool isWalking;
    public bool isDead;

    public Transform spawn;
    public Transform target;
    public float speed = 2f;
    private float range;

    public int dmg;
    private GameObject colGO;
    private float timer;
    public bool controle;

    public bool canWalk;
    public AnimationClip attackAnim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        transform.position = spawn.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        maxDist = 7;
        minDist = 1.1f;
        dmg = 10;
        colGO = null;
        speed = 3.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (canWalk)
        {
            anim.SetBool("isWalking", isWalking);
            anim.SetBool("isAttacking", isAttacking);
            isDead = anim.GetBool("Died");

            if (Vector2.Distance(transform.position, target.position) > maxDist)
                isWalking = false;

            if (range <= minDist && target == spawn)
                isWalking = false;

            else if (range > minDist && target == spawn)
                isWalking = true;

            if (anim.GetBool("Hurt") || anim.GetBool("isFurious"))
            {
                isAttacking = false;
                isWalking = false;
            }

            if (controle)
                timer += Time.deltaTime;

            if (timer >= attackAnim.length)
            {
                if (colGO != null && isAttacking && isDead == false)
                    colGO.GetComponent<PlayerHealth>().TakeDamage(dmg);

                isAttacking = false;
                isWalking = false;

                if (timer >= attackAnim.length * Random.Range(1.3f, 1.8f))
                {
                    controle = false;
                    timer = 0;
                }
            }

            if (GetComponent<EnemyHealth>().curHealth < GetComponent<EnemyHealth>().maxHealth)
            {
                maxDist = 15;
                GetComponent<EnemyAIRaycast>().ChangeDistAndSpeed(speed);
                GetComponent<EnemyHealth>().maxDistInv = 10;
            }

            range = Vector2.Distance(transform.position, target.position);

            if (range < maxDist && anim.GetBool("isFurious") == false && anim.GetBool("Hurt") == false && range != 0)
                isWalking = true;

            if (isWalking && isDead == false)
            {
                if (range <= minDist && isAttacking == false && timer == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().curHealth > 0)
                {
                    isAttacking = true;
                    controle = true;
                }

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().curHealth <= 0)
                {
                    target = spawn;
                    GetComponent<EnemyAIRaycast>().SetNewTarget(spawn);
                    isWalking = true;
                    minDist = 0;
                    GetComponent<EnemyAIRaycast>().ChangeDistAndSpeed(speed);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            colGO = col.gameObject;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
            colGO = null;
    }
}
