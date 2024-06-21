using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData
{
    public Vector3 cellCenter { get; private set; }

    public ItemSO itemSO { get; private set; }

    public CellData(Vector3 cellCenter)
    { 
        this.cellCenter = cellCenter;
    }

    public void SetItem(ItemSO item)
    {
        itemSO = item;
    }

    public void ClearItem()
    {
        itemSO = null;
    }

}
