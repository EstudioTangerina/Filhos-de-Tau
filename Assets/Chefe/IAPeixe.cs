using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPeixe : MonoBehaviour {

    private GameObject player;
    private float timer;
    private bool col;
    public int dmg = 15;
    public AnimationClip hurtAnim;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if(!col && GetComponent<EnemyHealth>().curHealth > 0)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);

        timer += Time.deltaTime;
        if(timer >= 10)
        {
            GetComponent<Animator>().SetBool("Died", true);
            Destroy(gameObject, hurtAnim.length + 0.2f);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        col = true;

        if (collision.gameObject.tag == "Player")
        {
            Vector2 pos = new Vector2(transform.position.x - player.gameObject.transform.position.x, transform.position.y - player.gameObject.transform.position.y);
            player.GetComponent<PlayerHealth>().TakeDamage(dmg);

            if (player.GetComponent<PlayerHealth>().curHealth > dmg)
                player.GetComponent<PlayerMovement>().Knockback(-pos.x, -pos.y);

            GetComponent<Animator>().SetBool("Died", true);
            Destroy(gameObject, hurtAnim.length + 0.2f);
        }
    }
}
