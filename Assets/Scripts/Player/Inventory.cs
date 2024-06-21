using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<ItemData> items = new List<ItemData>();
    public event Action<List<ItemData>> OnItemsUpdated;

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (!ContainsItem(itemSO, out ItemData data))
        {
            ItemData newItem = new ItemData(itemSO, quantity);
            items.Add(newItem);
        }
        else
        {
            data.AddQuantity(quantity);
        }
        OnItemsUpdated?.Invoke(items);
    }

    public bool TryRemoveItem(ItemSO itemSO, int quantity)
    {
        if (ContainsItem(itemSO, out ItemData data))
        {
            if (data.quantity > quantity)
            {
                data.AddQuantity(-quantity);
                OnItemsUpdated?.Invoke(items);
                return true;
            }
            else if (data.quantity == quantity)
            {
                items.Remove(data);
                OnItemsUpdated?.Invoke(items);
                return true;
            }
           
        }
        return false;
    }

    private bool ContainsItem(ItemSO itemSO, out ItemData data)
    { 
        foreach (ItemData itemData in items) 
        {
            if (itemData.itemSO.itemType == itemSO.itemType)
            {
                data = itemData;
                return true;
            }
        }
        data = null;
        return false;
    }

    public void ClearInventory()
    { 
        items.Clear();
    }

    public List<ItemData> GetItems()
    {
        List<ItemData> itemsCopy = new List<ItemData>();
        itemsCopy.AddRange(items);
        return itemsCopy;

    }
}


