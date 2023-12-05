using UnityEngine;
using UnityEngine.UI;

public class DynamicGridLayoutSetup : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;
    [SerializeField]
    private int _cellCountX = 4; // Number of cells in X direction
    [SerializeField]
    private int _cellCountY = 4; // Number of cells in Y direction
    [SerializeField]
    private int _spacing = 10;   // Spacing between cells

    public GridLayoutGroup GridLayoutGroup
    {
        get { return _gridLayoutGroup; }
        set
        {
            _gridLayoutGroup = value;
            UpdateGridLayout();
        }
    }

    public int CellCountX
    {
        get { return _cellCountX; }
        set
        {
            _cellCountX = value;
            UpdateGridLayout();
        }
    }

    public int CellCountY
    {
        get { return _cellCountY; }
        set
        {
            _cellCountY = value;
            UpdateGridLayout();
        }
    }

    public int Spacing
    {
        get { return _spacing; }
        set
        {
            _spacing = value;
            UpdateGridLayout();
        }
    }

    void Start()
    {
        if (GridLayoutGroup == null)
        {
            Debug.LogError("GridLayoutGroup reference not set!");
            return;
        }

        UpdateGridLayout();
    }

    public void pdate()
    {
        UpdateGridLayout();
    }

    private void UpdateGridLayout()
    {
        if (GridLayoutGroup != null)
        {
            float availableWidth = (Screen.width - (GridLayoutGroup.padding.left + GridLayoutGroup.padding.right));
            float availableHeight = (Screen.height - (GridLayoutGroup.padding.top + GridLayoutGroup.padding.bottom));

            float cellWidth = (availableWidth - (Spacing * (CellCountX - 1))) / CellCountX;
            float cellHeight = (availableHeight - (Spacing * (CellCountY - 1))) / CellCountY;

            GridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
            GridLayoutGroup.spacing = new Vector2(Spacing, Spacing);
        }
    }
}
