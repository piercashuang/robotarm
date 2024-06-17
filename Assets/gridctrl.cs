using UnityEngine;
using UnityEngine.UI;

public class gridctrl : MonoBehaviour
{
    public int numRows = 20;
    public int numColumns = 4;
    public GameObject cellPrefab;

    // 设置不同列之间的间距
    public float[] columnSpacings = { 20f, 30f, 80f };

    void Start()
    {
        GenerateTable();
    }

    void GenerateTable()
    {
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = numColumns;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                GameObject cell = Instantiate(cellPrefab, transform);
                Text cellText = cell.GetComponentInChildren<Text>();
                cellText.text = "Row: " + row + "\nColumn: " + col;
            }
        }

        // 设置不同列之间的间距
        for (int col = 0; col < numColumns - 1; col++)
        {
            GridLayoutGroup.Constraint constraint = gridLayout.constraint;
            gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            gridLayout.constraintCount = numRows;

            if (col < columnSpacings.Length)
            {
                gridLayout.spacing = new Vector2(columnSpacings[col], 0f);
            }

            gridLayout.constraint = constraint;
        }
    }
}
