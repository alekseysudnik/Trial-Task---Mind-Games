using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


public class ItemSelectionPresenter : MonoBehaviour
{
    [Inject] LevelManager levelManager;
    [Inject] private Player player;

    [SerializeField] private ItemSelectionButton buttonPrefab;
    [SerializeField] private Transform buttonsTransform;
    [SerializeField] private GameObject selectionPopup;

    private List<ItemSelectionButton> buttons = new List<ItemSelectionButton>();

    private void Start()
    {
        selectionPopup.SetActive(false);
        buttonsTransform.gameObject.SetActive(false);

        foreach (var gridManager in levelManager.gridList)
        {
            gridManager.OnReceiverInteract += GridManager_OnReceiverInteract;
        }
    }

    private void GridManager_OnReceiverInteract(Receiver receiver)
    {
        UpdateUI(player.inventory.GetItems(), receiver);
    }

    private void UpdateUI(List<ItemData> itemDataList, Receiver receiver)
    {
        if (itemDataList.Count == 0)
        {
            return;
        }

        selectionPopup.SetActive(true);
        buttonsTransform.gameObject.SetActive(true);

        ClearSlots();

        foreach (ItemData data in itemDataList)
        {
            ItemSelectionButton button = Instantiate(buttonPrefab, buttonsTransform);
            button.UpdateSlot(data);
            buttons.Add(button);
            button.GetButton().onClick.AddListener(() =>
            { 
                selectionPopup.SetActive(false);
                if (player.TryExtractObjectFromInventory(button.GetItemToPass(), out IPickable extractedObject))
                {
                    extractedObject.PutDown(receiver);
                }
            });
        }     
    }
    private void ClearSlots()
    {
        foreach (ItemSelectionButton button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
    }
}
