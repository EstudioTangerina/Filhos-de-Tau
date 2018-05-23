using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemScript")]
public class ItemScript : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public string description;

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
