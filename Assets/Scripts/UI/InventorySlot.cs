using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text quantityText;

    public void UpdateSlot(ItemData data)
    {
        itemIcon.sprite = data.itemSO.icon;
        quantityText.text = ("x" + data.quantity.ToString());
    }
}
