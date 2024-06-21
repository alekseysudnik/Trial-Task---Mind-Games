using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable: IInteraction
{
    public event Action OnItemPicked;
    public void PickUp(Transform picker);
    public void PutDown(Receiver receiver);
    public ItemSO GetPickableSO();

}
