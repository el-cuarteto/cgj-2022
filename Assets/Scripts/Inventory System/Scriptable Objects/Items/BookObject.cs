using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Book", menuName = "Inventory System/Items/Book")]
public class BookObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Book;
    }
}
