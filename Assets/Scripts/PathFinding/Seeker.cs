using UnityEngine;
using System.Collections.Generic;

public class Seeker : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private float updateRate = 0.25f;

    private List<Node> path;
    private int targetIndex;

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

    private void Update()
    {
        if (path != null)
        {
            FollowPath();
        }
    }

    void FollowPath()
    {
        if (targetIndex >= path.Count) return;

        Vector3 targetPos = path[targetIndex].Position;

        // Рухаємося
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        // Використовуємо sqrMagnitude для оптимізації (0.1f * 0.1f = 0.01f)
        if ((transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetIndex++;
        }
    }
}