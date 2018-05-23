using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {
    private SpriteRenderer player;
    private SpriteRenderer render;
    public bool canAim;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
        render = GetComponent<SpriteRenderer>();
        canAim = true;
    }
	
	// Update is called once per frame
	void Update () {
        render.sortingOrder = player.sortingOrder - 1;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if(canAim)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
