using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public float X_START = -240;
    public float Y_START = 0;
    public float Z_START = 0;
    public float ITEM_SIZE = 41;
    public float ITEM_MARGIN = 18;

    private Dictionary<ItemObject, GameObject> displayedItems = new Dictionary<ItemObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        displayItems();
    }

    // Update is called once per frame
    void Update()
    {
        displayItems();
    }

    private void displayItems()
    {
        int i = 0;
        foreach (var kv in inventory.itemsDict)
        {
            displayItem(kv.Value, i++);
        }
    }

    private void displayItem(ItemObject item, int i)
    {
        if ( !displayedItems.ContainsKey(item) )
        {
            var gameObject = Instantiate(item.prefab,
                Vector3.zero,
                Quaternion.identity,
                transform);
            gameObject.GetComponent<RectTransform>().localPosition = 
                new Vector3(X_START + i * (ITEM_SIZE + 2 * ITEM_MARGIN), Y_START, Z_START);
            displayedItems.Add(item, gameObject);
        }
    }
}
