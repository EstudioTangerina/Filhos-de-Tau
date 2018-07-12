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
    public GameObject colGO;
    public float timer;
    public bool controle;

    public bool canWalk;
    public AnimationClip attackAnim;

    public Vector2 knockbackDir;
    public bool knockbackActive;
    public float knockbackDirDist;
    public bool canReceiveKnockback;
    public float kncokbackTimer;
    // Use this for initialization
    void Start()
    {
        canReceiveKnockback = true;
        anim = GetComponent<Animator>();
        transform.position = spawn.position;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //maxDist = 7;
        //minDist = 1.1f;
        dmg = 10;
        colGO = null;
        //speed = 3.5f;
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
            {
                isWalking = false;
                GetComponent<EnemyAIRaycast>().enabled = false;
            }

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
                {
                    Vector2 pos = new Vector2(transform.position.x - colGO.gameObject.transform.position.x, transform.position.y - colGO.gameObject.transform.position.y);

                    if(colGO.GetComponent<PlayerHealth>().curHealth > dmg)
                        colGO.GetComponent<PlayerMovement>().Knockback(-pos.x, -pos.y);

                    colGO.GetComponent<PlayerHealth>().TakeDamage(dmg);
                    colGO.GetComponent<PlayerHealth>().Hurt(pos.x, pos.y);
                }
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

            if (range < maxDist && anim.GetBool("isFurious") == false && anim.GetBool("Hurt") == false && range > minDist)
                isWalking = true;

            if (!isDead)
            {
                if (colGO != null && !isAttacking && timer == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().curHealth > 0)
                {
                    isAttacking = true;
                    controle = true;
                    isWalking = false;
                }

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().curHealth <= 0 && target != spawn)
                {
                    colGO = null;
                    isAttacking = false;
                    isWalking = false;
                }
            }

            if(knockbackActive)
            {
                knockbackDirDist = Vector2.Distance(transform.position, knockbackDir);
                kncokbackTimer += Time.deltaTime;

                if(kncokbackTimer > 0.5f)
                {
                    knockbackActive = false;
                    kncokbackTimer = 0;
                }

                if (knockbackDirDist > 0)
                    transform.position = Vector2.MoveTowards(transform.position, knockbackDir, speed * 8 * Time.deltaTime);

                else
                    knockbackActive = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ColTag")
        {
            knockbackActive = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ColTag")
        {
            canReceiveKnockback = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ColTag")
        {
            canReceiveKnockback = true;
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

    public void Knockback(float x, float y)
    {
        if(canReceiveKnockback)
        {
            knockbackActive = true;
            knockbackDir = new Vector2(transform.position.x - (-x), transform.position.y - (-y));
            isAttacking = false;
            timer = 0;
            controle = false;
        }
    }
}