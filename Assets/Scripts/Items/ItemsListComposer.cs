using System;
using System.Collections.Generic;


public class ItemsListComposer 
{
    public List<ItemType?> CalculateRandomItemsList(int count)
    {
        List<ItemType?> items = new List<ItemType?>();
        for (int i = 0; i < count; ++i)
        {
            items.Add(GetRandomItemType(0));
        }
        return items;
    }

    public ItemType? GetRandomItemType(int minIndex)
    {
        int maxIndex = Enum.GetNames(typeof(ItemType)).Length;
        int index = UnityEngine.Random.Range(minIndex, maxIndex);
        if (index == 0)
        {
            return null;
        }
        return (ItemType)index;
    }
}
