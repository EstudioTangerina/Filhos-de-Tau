using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemName : MonoBehaviour {
    public string itemName;
    public float dist;
    private GameObject player;
    public GameObject itenNameText;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        itenNameText.GetComponent<TextMeshProUGUI>().text = itemName;
	}
	
	// Update is called once per frame
	void Update () {
        float d = Vector2.Distance(transform.position, player.transform.position);

        if (d <= dist)
            itenNameText.SetActive(true);

        else
            itenNameText.SetActive(false);
	}
}
