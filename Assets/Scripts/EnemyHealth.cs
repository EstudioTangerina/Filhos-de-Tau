using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
    public float curHealth;
    public float maxHealth;

    [SerializeField]
    private GameObject healthBar;

    [SerializeField]
    private GameObject healthBarCanvas;

    private float calcHealth;

    public float maxDistInv;

    public bool attacked;
    private float timer;

    [SerializeField]
    private AnimationClip hurtAnim;

    public bool isDoll;

    public RhythmBar rhythm;
    // Use this for initialization
    void Start () {
        curHealth = maxHealth;

        healthBarCanvas.SetActive(false);

        maxDistInv = 5;
    }

    private void FixedUpdate()
    {
        calcHealth = curHealth / maxHealth;
        healthBar.transform.localScale = new Vector3(calcHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBarCanvas.GetComponent<Canvas>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
    }

    // Update is called once per frame
    void Update () {/*
        if(curHealth < maxHealth && !isDoll)
            healthBarCanvas.SetActive(true);
        */
        if(attacked)
        {
            timer += Time.deltaTime;

            if(timer >= hurtAnim.length * 2 && hurtAnim != null)
            {
                if(!isDoll)
                    GetComponent<Animator>().SetBool("Hurt", false);

                attacked = false;
                timer = 0;
            }
        }

        if (curHealth <= 0)
        {
            if (!isDoll)
            {
                GetComponent<Animator>().SetBool("Died", true);
                Destroy(gameObject, 1);
            }

            healthBarCanvas.SetActive(false);
            curHealth = 0;
        }
        /*
        if (curHealth != 0)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (Vector2.Distance(transform.position, player.transform.position) > maxDistInv)
            {
                healthBarCanvas.SetActive(false);
            }

            else
            {
                if(!isDoll)
                    healthBarCanvas.SetActive(true);
            }
        }

        else
            healthBarCanvas.SetActive(false);*/
    }

    public void TakeDamage(int dano)
    {
        if (!isDoll)
        {
            if (GetComponent<Animator>().GetBool("isFurious") == false)
            {
                curHealth -= dano * rhythm.GetComponent<RhythmBar>().intensity;
                rhythm.GetComponent<RhythmBar>().freeze = true;
                if (Random.Range(1, 6) != 2 && attacked == false)
                {
                    attacked = true;
                    GetComponent<Animator>().SetBool("Hurt", true);
                }
            }
        }

        else
        {
            curHealth -= dano;
            GetComponent<RagDool>().attack = true;
        }
    }
}
