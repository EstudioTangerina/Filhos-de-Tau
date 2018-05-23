using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public Button removeButton;
    ItemScript item;
    public GameObject[] itens;

    public void Update()
    {
        if (Inventory.instance.removeAll)
            OnRemoveButton();
    }

    public void AddItem(ItemScript newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.preserveAspect = true;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        for(int i = 0; i < itens.Length; i++)
        {
            if(item != null)
            if(itens[i].name == item.name)
            {
                GameObject newItem = (GameObject)Instantiate(itens[i], GameObject.FindGameObjectWithTag("Player").transform.position, Quaternion.Euler(0,0,0));
                Inventory.instance.Remove(item);
                i = itens.Length;
            }
        }
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
            Inventory.instance.description.text = "";
        }
    }

    public void ShowDescription()
    {
        if (item != null)
            Inventory.instance.description.text = item.description;

        else
            Inventory.instance.description.text = "";
    }
}
