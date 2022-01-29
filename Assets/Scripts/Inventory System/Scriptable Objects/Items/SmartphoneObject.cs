using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Smartphone", menuName = "Inventory System/Items/Smartphone")]
public class SmartphoneObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Smartphone;
        description = "A computing platform portable device that combines mobile telephone and " +
            "computing functions into one unit. You can use it to speak with your friends around " +
            "the world, to play anime-inspired games or to watch people dancing.";
        Debug.Log(name);
    }
}
