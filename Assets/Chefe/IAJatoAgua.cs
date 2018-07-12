using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAJatoAgua : MonoBehaviour
{

    Vector3 _origPos;
    Vector3 _playerPos;
    public GameObject player;

    Vector3 giro;
    Vector3 batata;

    int timer;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        /*
        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //giro.z = transform.rotation.z;
        */

       

        batata = (transform.localPosition - player.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = batata;

        transform.Translate(dir * -1f * Time.deltaTime);

        timer++;

        if (timer >= 500)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().TakeDamage(15);
            
        }
    }
}