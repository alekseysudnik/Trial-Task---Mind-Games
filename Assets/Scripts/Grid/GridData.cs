using UnityEngine;

public class GridData 
{
    private int countX, countZ;
    private float cellSize;
    private Vector3 gridCenterPosition;
    private Vector3 gridOrigin;

    public CellData[,] cells { get; private set; }
    public GridData(int countX, int countZ, float cellSize, Vector3 gridCenterPosition)
    { 
        this.countX = countX;
        this.countZ = countZ;
        this.cellSize = cellSize;
        this.gridCenterPosition = gridCenterPosition;
    }

    public void InitializeGrid()
    { 
        cells = new CellData[countX, countZ];

        gridOrigin = new Vector3();
        gridOrigin.x = gridCenterPosition.x - countX * cellSize / 2;
        gridOrigin.y = gridCenterPosition.y;
        gridOrigin.z = gridCenterPosition.z - countZ * cellSize / 2;

        for (int i = 0; i < countX; ++i)
        {
            for (int j = 0; j < countZ; ++j)
            {
                cells[i, j] = new CellData(CalculateCellCenter(i,j));
            }
        }
    }

    private Vector3 CalculateCellCenter(int i, int j)
    { 
        Vector3 cellCenter = new Vector3();
        cellCenter.x = (0.5f + i) * cellSize;
        cellCenter.z = (0.5f + j) * cellSize;
        cellCenter += gridOrigin;
        return cellCenter;
    }

    public static bool CompareGrid(GridData compareToGrid, GridData pretendentGrid, out float similarity)
    {
        bool isIdentical = true;
        similarity = 0;
        int occupiedCellsCount = 0;

        foreach (CellData cell in compareToGrid.cells)
        {
            if (cell.itemSO != null)
            {
                occupiedCellsCount++;
            }
        }

        for (int i = 0; i < compareToGrid.cells.GetLength(0); ++i) 
        {
            for (int j = 0; j < compareToGrid.cells.GetLength(1); ++j)
            {
                if (compareToGrid.cells[i, j].itemSO != pretendentGrid.cells[i, j].itemSO)
                {
                    isIdentical = false;
                }
                else if (compareToGrid.cells[i, j].itemSO != null)
                {
                    similarity += 1f / occupiedCellsCount;
                }
            }
        }

        return isIdentical;
    }
}
