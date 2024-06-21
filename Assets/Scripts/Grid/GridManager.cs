using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GridManager : MonoBehaviour
{
    [Inject] ItemsFactory itemsFactory;

    [SerializeField] private Transform gridTransform;
    public GridData gridData { get; private set; }

    private Dictionary<CellData, Receiver> receiversDictionary = new Dictionary<CellData, Receiver>();

    private int countX, countZ;
    private float itemSpawnOffsetY;

    public event Action<Receiver> OnReceiverInteract;
    public event Action OnGridUpdated;

    public void InitializeGrid(SceneConfigSO configSO)
    {
        countX = configSO.countX;
        countZ = configSO.countZ;
        itemSpawnOffsetY = configSO.itemSpawnOffsetY;
        gridData = new GridData(countX, countZ, configSO.cellSize, gridTransform.localPosition);
        gridData.InitializeGrid();
        SpawnReceivers(gridTransform);

    }
    private void SpawnReceivers(Transform parent)
    {
        foreach (CellData cell in gridData.cells)
        {
            GameObject receiverObj = itemsFactory.CreateItem(ItemType.Receiver, cell.cellCenter, parent);
            if (receiverObj.TryGetComponent(out Receiver receiver))
            {
                Receiver newReceiver = receiver;
                receiversDictionary[cell] = receiver;
                newReceiver.OnReceiverInteract += Receiver_OnReceiverInteract;
                newReceiver.OnPickableUpdated += Receiver_OnPickableUpdated;
            }
        }
    }

    private void Receiver_OnPickableUpdated(Receiver receiver, IPickable pickable)
    {
        foreach (var pair in receiversDictionary)
        {
            if (pair.Value == receiver)
            {
                ItemSO newItem = null;
                if (pickable != null)
                {
                    newItem = pickable.GetPickableSO();
                }
                pair.Key.SetItem(newItem);
            }
        }
        OnGridUpdated?.Invoke();
    }

    private void Receiver_OnReceiverInteract(Receiver receiver)
    {
        OnReceiverInteract?.Invoke(receiver);
    }

    public void FillRandom(List<ItemType?> items, bool interactable)
    {
        List<ItemType?> itemsCopy = new List<ItemType?>();
        itemsCopy.AddRange(items);

        for (int i = 0; i < countX; ++i)
        {
            for (int j = 0; j < countZ; ++j)
            {
                int index = UnityEngine.Random.Range(0, itemsCopy.Count);
                ItemType? type = itemsCopy[index];
                receiversDictionary[gridData.cells[i, j]].SetInteractable(interactable);

                if (type != null) 
                {
                    GameObject newObject = itemsFactory.CreateItem((ItemType)type, gridData.cells[i, j].cellCenter + new Vector3(0, itemSpawnOffsetY, 0));
                    if(newObject.TryGetComponent(out IPickable pickableItem))
                    {
                        gridData.cells[i, j].SetItem(pickableItem.GetPickableSO());
                        pickableItem.SetInteractable(interactable);                    
                        receiversDictionary[gridData.cells[i,j]].SetPickableObject(pickableItem);
                        newObject.transform.SetParent(receiversDictionary[gridData.cells[i, j]].ReceivePoint);
                    }
                }
                itemsCopy.Remove(type);
            }
        }

    }
    public void ResetGrid(SceneConfigSO config)
    {
        UnsubscribeFromEvents();
        foreach (Receiver receiver in receiversDictionary.Values)
        {
            Destroy(receiver.gameObject);
        }
        receiversDictionary.Clear();

        InitializeGrid(config);
        OnGridUpdated?.Invoke();

    }
    private void UnsubscribeFromEvents()
    {
        foreach (Receiver receiver in receiversDictionary.Values)
        { 
            receiver.OnPickableUpdated -= Receiver_OnPickableUpdated;
            receiver.OnReceiverInteract -= Receiver_OnReceiverInteract;
        }
    }
    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
}
