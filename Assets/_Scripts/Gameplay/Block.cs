using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler
{
    [Header("Init data")]
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);

    [Header("Asigned Variable")]
    [SerializeField] private Board board;
    [SerializeField] private BlockHolder blockHolder;
    public CellColor blockColor = CellColor.Gray;

    private const int SIZE = 3;
    private Cell[,] blockCells = new Cell[SIZE, SIZE];

    private Vector2 initialPosition;

    private int currentShapeIndex = 0;

    public void Init(Board board, BlockHolder blockHolder)
    {
        this.board = board;
        this.blockHolder = blockHolder;

        for (int x = SIZE - 1; x >= 0; x--)
        {
            for (int y = SIZE - 1; y >= 0; y--)
            {
                GameObject cellIns = Instantiate(cellPrefab, transform);
                cellIns.transform.localPosition = new Vector3(-0.5f + 0.5f * x, 0.5f - 0.5f * y, 0);
                blockCells[x, y] = cellIns.GetComponent<Cell>();
                cellIns.GetComponent<Cell>().SetColor(blockColor);
            }
        }

        initialPosition = transform.position;
    }

    public void Show()
    {
        currentShapeIndex = Random.Range(0, AvaiableShape.GetShapeCount());
        int[,] shape = AvaiableShape.GetShape(currentShapeIndex);

        for (int x = 0; x < SIZE; x++)
        {
            for (int y = 0; y < SIZE; y++)
            {
                if (shape[x, y] == 1)
                {
                    blockCells[x, y].Show(blockColor);
                }
                else
                {
                    blockCells[x, y].Hide();
                }
            }
        }
    }

    public void DistanceCell(BlockStatus blockStatus)
    {
        if (blockStatus == BlockStatus.NotHold)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    blockCells[x, y].transform.localPosition = new Vector3(-0.5f + 0.5f * x, 0.5f - 0.5f * y, 0);
                }
            }
        }
        else if (blockStatus == BlockStatus.Hold)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    blockCells[x, y].transform.localPosition = new Vector3(-1f + 1f * x, 1f - 1f * y, 0);
                }
            }
        }
    }

    public void SetSizeCell(BlockStatus blockStatus)
    {
        if (blockStatus == BlockStatus.Hold)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    blockCells[x, y].SetSize(0.5f);
                }
            }
        }
        else if (blockStatus == BlockStatus.NotHold)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    blockCells[x, y].SetSize(0.25f);
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetSizeCell(BlockStatus.Hold);
        DistanceCell(BlockStatus.Hold);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);

        transform.position = mousePos + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);

        transform.position = mousePos + offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        if (TryGetIndex(transform.position, out Vector2Int index))
        {
            board.Preview(index, currentShapeIndex, blockColor);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetSizeCell(BlockStatus.NotHold);
        DistanceCell(BlockStatus.NotHold);

        if (TryGetIndex(transform.position, out Vector2Int index) && board.Place(index, currentShapeIndex, blockColor))
        {
            blockHolder.DespawnBlock(this);
            EventManager.Instance.OnBlockPlaced?.Invoke();
        } else
        {
            transform.position = initialPosition;
        }
    }

    private bool TryGetIndex(Vector3 centerPos, out Vector2Int index)
    {
        float firstCell = -3.5f;

        int x = Mathf.RoundToInt(centerPos.x - firstCell);
        int y = Mathf.RoundToInt(centerPos.y - firstCell);

        index = new Vector2Int(x, y);

        return x >= 0 && x < 8 &&
           y >= 0 && y < 8;
    }
}

public enum BlockStatus
{
    None,
    Hold,
    NotHold
}