using UnityEngine;
using static Pathfinding;

public class Node : IHeapItem<Node>
{
    public int HeapIndex { get; set; }

    public Vector2 Position;
    public Node Parent;
    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;
    public bool Walkable;
    public int GridX, GridY;
    public Node(Vector2 position, bool walkable, int gridX, int gridY)
    {
        Position = position;
        Walkable = walkable;
        GridX = gridX;
        GridY = gridY;
    }
    public int CompareTo(Node other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0)
        {
            compare = HCost.CompareTo(other.HCost);
        }
        return -compare; // Повертаємо інверсію для Min-Heap
    }
}
