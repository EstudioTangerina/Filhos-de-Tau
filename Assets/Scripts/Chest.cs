using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {
    public bool opened;
    public bool showItens;
    public bool recentlyOpened;

    public GameObject itemContainer;

	// Use this for initialization
	void Start () {
        itemContainer = transform.GetChild(0).gameObject;
        itemContainer.SetActive(showItens);
    }
	
	// Update is called once per frame
	void Update () {
        itemContainer.SetActive(showItens);
    }
}
