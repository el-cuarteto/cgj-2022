using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWithInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void Update()
    {
        // For development purposes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Clear();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        ItemForInventory item = other.GetComponent<ItemForInventory>();
        if (item)
        {
            inventory.AddItem(item.item);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.itemsDict.Clear();
    }
}
