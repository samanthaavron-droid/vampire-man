using UnityEngine;
using System.Collections.Generic;

public class Seeker : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private float updateRate = 0.25f;

    internal List<Node> path;
    internal int targetIndex;

    private Vector2 _currentDirection;
    private Vector2 _lastDirection;

    private void Start()
    {
        // Оновлюємо шлях 4 рази на секунду
        RequestPath();
    }

    void RequestPath()
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

    private void Update()
    {
        //if (MovementSys.CanMove(gameObject, _currentDirection))
        //    MovementSys.Move(gameObject, _currentDirection, 1);

        if (path == null || path.Count == 0 || target == null)
        {
            RequestPath();
            return;
        }

        float distance = Vector2.Distance(transform.position, path[targetIndex].Position);

        if (distance < 0.15f)
        {
            RequestPath();
        }
        if (path != null && path.Count > 0)
        {
            Vector3 dir = path[targetIndex].Position - (Vector2)transform.position;
            _currentDirection = MovementSys.GetDirection(dir);
            transform.position += (Vector3)_currentDirection * speed * Time.deltaTime;
        }
    }
}