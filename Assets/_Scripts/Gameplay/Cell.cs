using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Sprite graySprite;
    [SerializeField] private Sprite whiteSprite;
    [SerializeField] private Sprite pinkSprite;
    [SerializeField] private Sprite orangeSprite;
    [SerializeField] private Sprite yellowSprite;
    [SerializeField] private Sprite purpleSprite;
    [SerializeField] private Sprite greenSprite;
    [SerializeField] private Sprite blueSprite;
    [SerializeField] private Sprite redSprite;

    [Header("Cell Size")]
    [SerializeField] private float size = 0.5f;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Need to fix later
    public void Show()
    {
        gameObject.SetActive(true);
        SetColor(CellColor.Gray);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        SetSize(size);
    }

    public void Hover()
    {
        gameObject.SetActive(true);
        SetColor(CellColor.Gray);
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetSize(float newSize)
    {
        spriteRenderer.transform.localScale = new Vector3(newSize, newSize, 1f);
    }

    public void SetColor(CellColor color)
    {
        switch (color)
        {
            case CellColor.Gray:
                spriteRenderer.sprite = graySprite;
                break;
            case CellColor.White:
                spriteRenderer.sprite = whiteSprite;
                break;
            case CellColor.Pink:
                spriteRenderer.sprite = pinkSprite;
                break;
            case CellColor.Orange:
                spriteRenderer.sprite = orangeSprite;
                break;
            case CellColor.Yellow:
                spriteRenderer.sprite = yellowSprite;
                break;
            case CellColor.Purple:
                spriteRenderer.sprite = purpleSprite;
                break;
            case CellColor.Green:
                spriteRenderer.sprite = greenSprite;
                break;
            case CellColor.Blue:
                spriteRenderer.sprite = blueSprite;
                break;
            case CellColor.Red:
                spriteRenderer.sprite = redSprite;
                break;
        }
    }
}

public enum CellColor
{
    Gray,
    White,
    Pink,
    Orange,
    Yellow,
    Purple,
    Green,
    Blue,
    Red
}
