using UnityEngine;
using System.Collections.Generic;

public class Seeker : MonoBehaviour
{
    public Transform target;

    internal List<Node> path;
    internal int targetIndex;

    private void Start()
    {
        // Оновлюємо шлях 4 рази на секунду
        RequestPath();
    }

    public void RequestPath()
    {
        if (target != null && Pathfinding.instance != null)
        {
            // Шукаємо шлях
            path = Pathfinding.instance.FindPath(transform.position, target.position);

            if (path != null && path.Count > 0)
            {
                targetIndex = 0;
            }

        }
    }
}