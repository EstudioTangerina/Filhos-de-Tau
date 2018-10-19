using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour {
    public float curHealth;
    public float maxHealth;

    [SerializeField]
    private GameObject healthBar;

    [SerializeField]
    private GameObject healthBarCanvas;

    public bool isBoss;
    private float calcHealth;

    public float maxDistInv;

    public bool attacked;
    private float timer;

    [SerializeField]
    private AnimationClip hurtAnim;

    public bool isDoll;

    public RhythmBar rhythm;

    public bool controle;

    public AudioSource hitSong;

    public bool hitByArrow;
    // Use this for initialization
    void Start () {
        curHealth = maxHealth;

        if (healthBarCanvas != null)
            healthBarCanvas.SetActive(false);

        maxDistInv = 5;
    }

    private void FixedUpdate()
    {
        calcHealth = curHealth / maxHealth;

        if (healthBarCanvas != null && !isBoss)
        {
            healthBar.transform.localScale = new Vector3(calcHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
            healthBarCanvas.GetComponent<Canvas>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if(isBoss)
            healthBar.GetComponent<Image>().fillAmount = calcHealth;
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

                hitByArrow = false;
                attacked = false;
                timer = 0;
            }
        }

        if (curHealth <= 0)
        {
            if (!isDoll && !isBoss)
            {
                if(GetComponent<AudioSource>().isPlaying == false)
                    GetComponent<AudioSource>().Play();

                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Contains(this.gameObject))
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().colGOList.Remove(this.gameObject);

                rhythm.gameObject.SetActive(false);
                GetComponent<Animator>().SetBool("Died", true);
                //GetComponent<Collider2D>().enabled = false;

                Destroy(gameObject, GetComponent<AudioSource>().clip.length);
            }

            else if(isBoss)
            {
                healthBarCanvas.GetComponent<Animator>().enabled = true;
                GetComponent<GuideBoss>().beated = true;
            }

            if (healthBarCanvas != null)
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
        if (controle && hitByArrow)
            PlayHitSong();

        if (!isDoll && !isBoss)
        {
            if (rhythm.gameObject.activeSelf)
            {
                curHealth -= dano * rhythm.GetComponent<RhythmBar>().intensity;
                rhythm.GetComponent<RhythmBar>().freeze = true;
                GetComponent<EnemyAI>().timer = 0;
            }

            else
                curHealth -= dano;

            attacked = true;
            GetComponent<Animator>().SetBool("Hurt", true);
        }

        else if(isBoss)
        {
			curHealth -= dano;
            attacked = true;

            float x = Random.Range(0, 2);
            Debug.Log(x);
            if(x == 1)
                GetComponent<GuideBoss>().MoveAgain();

            GetComponent<Animator>().SetBool("Hurt", true);

            GetComponent<GuideBoss>().tempoParado = 0;

            if (GetComponent<GuideBoss>().ataque == false)
            {
                GetComponent<GuideBoss>().timer =  GetComponent<GuideBoss>().limite;
            }
        }

        else
        {
            curHealth -= dano;
            GetComponent<RagDool>().attack = true;
        }
    }

    void PlayHitSong()
    {
        if (hitSong != null)
        {
            hitSong.Play();
            hitByArrow = false;
        }
    }

    public void Knockback(float x, float y)
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-x * 1000, -y * 1000), ForceMode2D.Force);
    }
}
