using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemsDictionary itemsDict = new ItemsDictionary();

    public void AddItem(ItemObject item)
    {
        if (HasItem(item)) {
            return;
        }

        itemsDict.Add(item.name, item);

        var builder = new StringBuilder();
        builder.AppendLine("Add " + item.name);
        builder.AppendFormat("There are {0} items\n", itemsDict.Count);
        foreach(var kv in itemsDict)
        {
            builder.AppendFormat(string.Format("<{0}, {1}>, ", kv.Key, kv.Value));
        }
        Debug.Log(builder.ToString());
    }

    public void RemoveItem(string name)
    {
        itemsDict.Remove(name);
    }

    public bool HasItem(ItemObject item)
    {
        return itemsDict.ContainsValue(item);
    }

    public void RemoveItem(ItemObject item)
    {
        RemoveItem(item.name);
    }

    public void Clear()
    {
        itemsDict.Clear();
    }

    /**
     * Used for serializing in the inspector
     */
    [Serializable]
    public class ItemsDictionary : Dictionary<string, ItemObject>, ISerializationCallbackReceiver
    {
        public List<string> _keys = new List<string>();
        public List<ItemObject> _values = new List<ItemObject>();

        public void OnBeforeSerialize()
        {
            _keys.Clear();
            _values.Clear();

            foreach (var kvp in this)
            {
                _keys.Add(kvp.Key);
                _values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();
            for (var i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            {
                Add(_keys[i], _values[i]);
            }
        }
    }
}
