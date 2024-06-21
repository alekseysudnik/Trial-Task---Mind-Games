using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform inventoryTransform;

    [Inject] private Player player;

    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        player.inventory.OnItemsUpdated += Inventory_OnItemsUpdated;
    }

    private void Inventory_OnItemsUpdated(List<ItemData> itemDataList)
    {
        UpdateUI(itemDataList);
    }

    private void UpdateUI(List<ItemData> itemDataList)
    {
        ClearSlots();

        foreach (ItemData data in itemDataList)
        {
            InventorySlot slot = Instantiate(slotPrefab, inventoryTransform);
            slot.UpdateSlot(data);
            slots.Add(slot);
        }
    }

    private void ClearSlots()
    { 
        foreach(InventorySlot slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
    }
}
