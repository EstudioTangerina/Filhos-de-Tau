using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReOrderLayer : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    public float offset;
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderer.sortingOrder = Mathf.FloorToInt((transform.position.y - offset) * -1);
    }
}
