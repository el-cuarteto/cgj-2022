using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Object", menuName = "Inventory System/Item")]
public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    [TextArea(15, 20)]
    public string description;
}
