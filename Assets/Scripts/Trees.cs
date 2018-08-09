using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    private SpriteRenderer renderer;
    private Color color;
    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        color = renderer.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            color.a = 0.67f;
            renderer.color = color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            color.a = 1;
            renderer.color = color;
        }
    }
}
