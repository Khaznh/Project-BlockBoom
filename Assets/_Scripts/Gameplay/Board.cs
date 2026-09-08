using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform cellHolder;

    private int size = 8;
    private Cell[,] boardCells;
    private CellState[,] boardCellStates;

    [SerializeField] private List<int> deleteRow = new();
    [SerializeField] private List<int> deleteCol = new();

    private void Awake()
    {
        boardCells = new Cell[size, size];
        boardCellStates = new CellState[size, size];

        EventManager.Instance.OnBlockPlaced += HandleLogicAfterPlace;
    }

    private void Start()
    {
        SpawnBoard();
    }

    public void Preview(Vector2Int index, int shapeIndex, CellColor blockColor = CellColor.Gray)
    {
        ClearPreview();

        if (IsPlaceable(index, shapeIndex))
        {
            int[,] shape = AvaiableShape.GetShape(shapeIndex);
            int length = shape.GetLength(1);
            int width = shape.GetLength(0);
            int milestoneX = width / 2;
            int milestoneY = length / 2;
            for (int i = -milestoneX; i <= milestoneX; i++)
            {
                for (int j = -milestoneY; j <= milestoneY; j++)
                {
                    int indexX = index.x + i;
                    int indexY = index.y - j;
                    if (shape[i + milestoneX, j + milestoneY] == 1)
                    {
                        boardCells[indexX, indexY].Hover();
                        boardCellStates[indexX, indexY] = CellState.Preview;
                    }
                }
            }
        }
    }

    public bool Place(Vector2Int index, int shapeIndex, CellColor blockColor = CellColor.Gray)
    {
        if (IsPlaceable(index, shapeIndex))
        {
            int[,] shape = AvaiableShape.GetShape(shapeIndex);
            int length = shape.GetLength(1);
            int width = shape.GetLength(0);
            int milestoneX = width / 2;
            int milestoneY = length / 2;
            for (int i = -milestoneX; i <= milestoneX; i++)
            {
                for (int j = -milestoneY; j <= milestoneY; j++)
                {
                    int indexX = index.x + i;
                    int indexY = index.y - j;
                    if (shape[i + milestoneX, j + milestoneY] == 1)
                    {
                        boardCells[indexX, indexY].Show();
                        boardCellStates[indexX, indexY] = CellState.Show;
                    }
                }
            }

            return true;
        }

        return false;
    }

    private void SpawnBoard()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                GameObject cellIns = Instantiate(cellPrefab, cellHolder);
                cellIns.transform.localPosition = new Vector3(-3.5f + x, -3.5f + y, 0);
                boardCells[x, y] = cellIns.GetComponent<Cell>();
                boardCells[x, y].Hide();
            }
        }
    }

    private void ClearPreview()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (boardCellStates[x, y] == CellState.Preview)
                {
                    boardCells[x, y].Hide();
                    boardCellStates[x, y] = CellState.Hide;
                }
            }
        }
    }

    private bool IsPlaceable(Vector2Int index, int shapeIndex)
    {
        if (index.x < 0 || index.x >= size || index.y < 0 || index.y >= size)
        {
            return false;
        }

        bool isPlaceable = true;

        int[,] shape = AvaiableShape.GetShape(shapeIndex);
        int length = shape.GetLength(1);
        int width = shape.GetLength(0);


        int milestoneX = width / 2;
        int milestoneY = length / 2;

        for (int i = -milestoneX; i <= milestoneX; i++)
        {
            for (int j = -milestoneY; j <= milestoneY; j++)
            {
                int indexX = index.x + i;
                int indexY = index.y - j;

                if (indexX < 0 || indexX >= size || indexY < 0 || indexY >= size)
                {
                    isPlaceable = false;
                    break;
                }

                if (shape[i + milestoneX, j + milestoneY] == 1 && boardCellStates[indexX, indexY] == CellState.Show)
                {
                    isPlaceable = false;
                    break;
                }
            }
            if (!isPlaceable)
            {
                break;
            }
        }

        return isPlaceable;
    }

    private void HandleLogicAfterPlace()
    {
        CheckFilledRowsAndColumns();

        ClearRowsAndColumns();
    }

    private void CheckFilledRowsAndColumns()
    {
        ClearFill();

        for (int i = 0; i < size; i++)
        {
            bool isColFilled = true;
            for (int j = 0; j < size; j++)
            {
                if (boardCellStates[i, j] != CellState.Show)
                {
                    isColFilled = false;
                }
            }

            if (isColFilled)
            {
                deleteCol.Add(i);
            }
        }

        for (int i = 0; i < size; i++)
        {
            bool isColFilled = true;
            for (int j = 0; j < size; j++)
            {
                if (boardCellStates[j, i] != CellState.Show)
                {
                    isColFilled = false;
                }
            }

            if (isColFilled)
            {
                deleteRow.Add(i);
            }
        }
    }

    private void ClearRowsAndColumns()
    {
        for (int i = 0; i < deleteCol.Count; i++)
        {
            int colIndex = deleteCol[i];
            for (int j = 0; j < size; j++)
            {
                boardCells[colIndex, j].Hide();
                boardCellStates[colIndex, j] = CellState.Hide;
                EventManager.Instance.OnColumnOrRowComplete?.Invoke();
            }
        }

        for (int i = 0; i < deleteRow.Count; i++)
        {
            int rowIndex = deleteRow[i];
            for (int j = 0; j < size; j++)
            {
                boardCells[j, rowIndex].Hide();
                boardCellStates[j, rowIndex] = CellState.Hide;
                EventManager.Instance.OnColumnOrRowComplete?.Invoke();
            }
        }
    }

    private void ClearFill()
    {
        deleteCol.Clear();
        deleteRow.Clear();  
    }
}

public enum CellState
{
    Hide,
    Preview,
    Show
}
