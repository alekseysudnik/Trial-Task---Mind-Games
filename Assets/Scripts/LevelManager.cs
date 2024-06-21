using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager: MonoBehaviour
{
    [SerializeField] private SceneConfigSO configSO;
    [SerializeField] private GridManager taskGrid;
    [SerializeField] private GridManager collectGrid;
    [SerializeField] private GridManager repeatGrid;

    public List<GridManager> gridList { get; private set; }
    private ItemsListComposer itemsListComposer = new ItemsListComposer();

    public event Action<float> OnCompletionProgressChanged;

    private void Awake()
    {
        InitializeGridData();
        FillGrid();

        repeatGrid.OnGridUpdated += RepeatGrid_OnGridUpdated;
    }

    private void InitializeGridData()
    {
        gridList = new List<GridManager>();
        gridList.Add(taskGrid);
        gridList.Add(collectGrid);
        gridList.Add(repeatGrid);

        foreach (GridManager grid in gridList) 
        {
            grid.InitializeGrid(configSO);
        }
    }

    private void FillGrid()
    {
        int cellsCount = configSO.countX * configSO.countZ;
        List<ItemType?> randomItems = itemsListComposer.CalculateRandomItemsList(cellsCount);
        List<ItemType?> randomItemsAugmented = new List<ItemType?>();
        taskGrid.FillRandom(randomItems, false);

        foreach (var item in randomItems)
        {
            if (item != null)
            {
                randomItemsAugmented.Add(item);
            }
            else
            {
                randomItemsAugmented.Add(itemsListComposer.GetRandomItemType(1));
            }
        }
        collectGrid.FillRandom(randomItemsAugmented, true);

    }
    private void RepeatGrid_OnGridUpdated()
    {
        bool levelCompleted = GridData.CompareGrid(taskGrid.gridData, repeatGrid.gridData, out float similarity);
        OnCompletionProgressChanged?.Invoke(similarity);
    }

    public void ResetLevel()
    {
        foreach (GridManager grid in gridList)
        {
            grid.ResetGrid(configSO);
        }
        FillGrid();
    }

    public void OnDisable()
    {
        repeatGrid.OnGridUpdated-= RepeatGrid_OnGridUpdated;
    }


}
