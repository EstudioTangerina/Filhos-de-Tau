using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    [HideInInspector]
    public bool canOpenInv;

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;

    public bool hover;
    private GameManager manager;
    [SerializeField]
    private KeyCode invButton;
    [SerializeField]
    private KeyCode selectButton;
    [SerializeField]
    private KeyCode rollButton;
    [SerializeField]
    private KeyCode specialButton;
    // Use this for initialization
    void Start () {
        if (FindObjectOfType<GameManager>() != null)
        {
            manager = FindObjectOfType<GameManager>();
            invButton = manager.buttons[8];
            selectButton = manager.buttons[5];
            rollButton = manager.buttons[7];
            specialButton = manager.buttons[9];
        }

        inventory = Inventory.instance;
        inventory.onIntemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        inventoryUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(invButton) && canOpenInv)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }

        if (Input.GetKeyDown(selectButton) && !hover || Input.GetKeyDown(rollButton) || Input.GetKeyDown(specialButton) && !hover)
            inventoryUI.SetActive(false);

        if (inventoryUI.activeSelf == false)
        {
            inventory.description.text = "";
            hover = false;
        }
    }

    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i ++)
        {
            if(i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }

            else
            {
                slots[i].ClearSlot();
            }
        }

        //Debug.Log("UPDATING UI");
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
    }

    public void ClearDescription()
    {
        inventory.description.text = "";
    }

    public void Selected(bool state)
    {
        hover = state;
    }
}
