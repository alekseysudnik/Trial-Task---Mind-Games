using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemsFactory: MonoBehaviour
{
    [Serializable]
    public class FactoryItem
    { 
        public ItemType type;
        public ItemSO itemSO;
    }

    [SerializeField] private List<FactoryItem> items = new List<FactoryItem>();

    public GameObject CreateItem(ItemType type, Vector3 position, Transform parent = null)
    {
        return Instantiate(GetItemByType(type).prefab, position, Quaternion.identity, parent);
    }

    private ItemSO GetItemByType(ItemType type)
    { 
        foreach (var item in items)
        {
            if (item.type == type)
            {
                return item.itemSO;
            }
        }
        return null;
    }

}
public enum ItemType
{
    Receiver = 0,
    BlueCube,
    RedCube,
}
