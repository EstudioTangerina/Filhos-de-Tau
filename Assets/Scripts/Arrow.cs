using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {
    public float arrowVel;
    public int dmg;
    private bool controle;
	// Use this for initialization
	void Start () {
        controle = true;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(arrowVel * Time.deltaTime, 0, 0);

        Destroy(this.gameObject, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && controle)
        {
            if (transform.rotation.z == 90 || transform.rotation.z == -90)
            {
                if (collision.gameObject.transform.position.y < transform.position.y + 0.5f)
                {
                    collision.GetComponent<EnemyHealth>().TakeDamage(dmg);
                    controle = false;
                    Destroy(gameObject, 0.05f);
                }
            }
            else
            {
                collision.GetComponent<EnemyHealth>().TakeDamage(dmg);
                controle = false;
                Destroy(gameObject, 0.05f);
            }
        }

        if (collision.gameObject.tag == "Untagged")
            Destroy(gameObject);
    }
}
