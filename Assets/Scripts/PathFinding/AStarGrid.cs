using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    public Vector2 GridSize = new Vector2(10, 10);
    public float NodeSize = 1f;
    public LayerMask ObstacleLayer;

    // Зробив public, щоб Pathfinding міг туди писати, а Gizmos читати
    public List<Node> Path;

    protected Node[,] grid;
    protected int gridSizeX, gridSizeY;

    protected virtual void Awake()
    {
        gridSizeX = Mathf.RoundToInt(GridSize.x / NodeSize);
        gridSizeY = Mathf.RoundToInt(GridSize.y / NodeSize);
        grid = new Node[gridSizeX, gridSizeY];
        CreateGrid();
    }

    void CreateGrid()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * GridSize.x / 2 - Vector3.up * GridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * NodeSize + NodeSize / 2) + Vector3.up * (y * NodeSize + NodeSize / 2);
                bool walkAble = Physics2D.OverlapBox(worldPoint, new Vector2(NodeSize - 0.1f, NodeSize - 0.1f), 0, ObstacleLayer) == null;
                grid[x, y] = new Node(worldPoint, walkAble, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighBours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.GridX + x;
                int checkY = node.GridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    // --- ВИПРАВЛЕННЯ ПРОХОДЖЕННЯ КРІЗЬ СТІНИ ---
                    // Якщо рух по діагоналі
                    if (Mathf.Abs(x) == 1 && Mathf.Abs(y) == 1)
                    {
                        // Перевіряємо сусідів по боках. Якщо хоч один стіна - діагональ блокується
                        Node node1 = grid[node.GridX + x, node.GridY]; // Сусід по горизонталі
                        Node node2 = grid[node.GridX, node.GridY + y]; // Сусід по вертикалі

                        if (!node1.Walkable || !node2.Walkable)
                            continue;
                    }

                    neighBours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighBours;
    }

    public Node GetNodeFromWorldPosition(Vector3 position)
    {
        float localX = position.x - transform.position.x;
        float localY = position.y - transform.position.y;
        float percentX = (localX + GridSize.x / 2) / GridSize.x;
        float percentY = (localY + GridSize.y / 2) / GridSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        // --- ВИПРАВЛЕННЯ: " - 1", щоб не вийти за межі масиву ---
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector3(GridSize.x, GridSize.y, 0));

        if (grid == null) return;

        foreach (Node node in grid)
        {
            if (node == null) continue;
            Gizmos.color = node.Walkable ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);

            // Тепер Path буде малюватись, бо ми його заповнюємо в Pathfinding
            if (Path != null && Path.Contains(node))
                Gizmos.color = Color.blue;

            Gizmos.DrawCube(node.Position, new Vector3(NodeSize - 0.05f, NodeSize - 0.05f, 0f));
        }
    }
}