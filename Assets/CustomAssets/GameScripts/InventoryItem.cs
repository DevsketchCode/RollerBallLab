using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryItem : MonoBehaviour, IComparable<InventoryItem>
{

    [SerializeField]
    private string itemName = "";

    private int quantity;

    public InventoryItem(string newName)
    {
        itemName = newName;
    }

    public int CompareTo(InventoryItem otherItem)
    {
        if (otherItem == null) return 1;
        return itemName.CompareTo(otherItem.itemName);
    }

    public void deductQuantity()
    {
        quantity -= 1;
    }

    public void commitQuantity()
    {
        quantity += 1;
    }

    public int getQuantity()
    {
        return quantity;
    }

    public string getItemName()
    {
        return itemName;
    }
}
