using System.IO;
using UnityEngine;

public class EnemyAI : Seeker
{
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

        // Використовуємо sqrMagnitude для оптимізації (0.1f * 0.1f = 0.01f)
        if ((transform.position - targetPos).sqrMagnitude < 0.01f)
        {
            targetIndex++;
        }
    }
}
