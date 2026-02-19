using UnityEngine;
using System.Collections.Generic;

public class Seeker : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private float updateRate = 0.25f;

    internal List<Node> path;
    internal int targetIndex;

    private void Start()
    {
        // Щоб не було null помилок, чекаємо трохи, поки сітка згенерується
        InvokeRepeating(nameof(RequestPath), 0.1f, updateRate);
    }

    void RequestPath()
    {
        if (target != null && Pathfinding.instance != null)
        {
            // Отримуємо і зберігаємо шлях
            path = Pathfinding.instance.FindPath(transform.position, target.position);

            if (path != null && path.Count > 0)
            {
                targetIndex = 0;
            }
        }
    }
}