using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUP : MonoBehaviour {
    public float range;
    private Transform target;

    public ItemScript item;

    public bool picked;

    public bool OnChest;
    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        range = Vector2.Distance(transform.position, target.position);
    }

    public void PickUP()
    {
        if(item.name == "Arrow")
        {
            target.GetComponent<PlayerMovement>().ammo += 1;
            Destroy(gameObject);
            return;
        }

        //Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            if(item.name != "Arrow")
                FindObjectOfType<InventoryUI>().inventoryUI.SetActive(true);

            Destroy(gameObject);
        }

        else
            FindObjectOfType<InventoryUI>().inventoryUI.SetActive(true);

    }
}
