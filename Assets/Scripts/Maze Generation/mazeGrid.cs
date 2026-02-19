using UnityEngine;

public class mazeGrid : MonoBehaviour
{
    private int width;
    private int height;
    private int cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;

    public mazeGrid(int width, int height, int cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        for (int i = 0; i < gridArray.GetLength(0); i++)
        {
            for (int j = 0; j < gridArray.GetLength(1); j++)
            {
                
            }
        }
    }
    public int GetCellSize()
    {
        return cellSize; 
    }
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * cellSize + originPosition;
    }
    public int[] GetXY(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        int y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);

        return new int[]{x, y};
    }
}
