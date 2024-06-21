using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button button;

    private ItemSO itemToPass;
    public void UpdateSlot(ItemData data)
    {
        button.onClick.RemoveAllListeners();
        itemIcon.sprite = data.itemSO.icon;
        itemToPass = data.itemSO;
    }

    public ItemSO GetItemToPass()
    { 
        return itemToPass;
    }
    public Button GetButton()
    {
        return button;
    }
}
