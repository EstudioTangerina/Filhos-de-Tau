using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float curHealth;
    public float maxHealth;

    [SerializeField]
    private GameObject healthBar;

    [SerializeField]
    private GameObject healthBarCanvas;

    private float calcHealth;
    private float hurtTimer;
    private float explosionTimer;
    // Use this for initialization
    void Start()
    {
        maxHealth = 100;
        curHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        calcHealth = curHealth / maxHealth;
        healthBar.GetComponent<Image>().fillAmount = calcHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (explosionTimer > 0.95f && explosionTimer < 1.2f)
        {
            GetComponent<PlayerMovement>().songs[9].Play();
            explosionTimer = 2;
        }


        if (curHealth == 0)
        {
            if(explosionTimer < 1.2f)
                explosionTimer += Time.deltaTime;

            GetComponent<Animator>().SetBool("Died", true);
            healthBarCanvas.SetActive(false);
        }

        if (GetComponent<Animator>().GetBool("Hurt"))
        {
            hurtTimer += Time.deltaTime;

            if (hurtTimer > 0.2f)
            {
                GetComponent<Animator>().SetBool("Hurt", false);
                hurtTimer = 0;
            }
        }
    }

    public void TakeDamage(int dano)
    {
        curHealth -= dano;
    }

    public void Hurt(float x, float y)
    {
        GetComponent<Animator>().SetBool("Hurt", true);
        GetComponent<Animator>().SetFloat("x", x);
        GetComponent<Animator>().SetFloat("y", y);
    }
}
