using UnityEngine;
using System.Collections.Generic;
using System;

public class Pathfinding : AStarGrid
{
    public static Pathfinding instance;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = GetNodeFromWorldPosition(startPos);
        Node targetNode = GetNodeFromWorldPosition(targetPos);

        if (!startNode.Walkable || !targetNode.Walkable)
        {
            // Якщо ціль або старт у стіні — шляху немає
            return null;
        }

        Heap<Node> openSet = new Heap<Node>(gridSizeX * gridSizeY);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                // --- ВИПРАВЛЕННЯ: Зберігаємо результат у змінну батька ---
                List<Node> resultPath = RetracePath(startNode, targetNode);
                Path = resultPath; // Тепер Gizmos побачить шлях!
                return resultPath;
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else
                        openSet.UpdateItem(neighbour);
                }
            }
        }
        return null;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();
        return path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);
        if (distX > distY) return 14 * distY + 10 * (distX - distY);
        return 14 * distX + 10 * (distY - distX);
    }
}
public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}