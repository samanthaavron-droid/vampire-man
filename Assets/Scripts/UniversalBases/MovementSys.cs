using UnityEngine;

public static class MovementSys
{
    private const float GridStep = 0.5f; // Зазвичай 1.0, змініть на 0.5 якщо у вас дрібна сітка

    // Перевантаження для зручності
    public static bool CanMove(GameObject entity, Vector2 direction)
    {
        return CanMove(entity.transform.position, direction);
    }

    public static bool CanMove(Vector2 pos, Vector2 direction)
    {
        if (direction == Vector2.zero) return false;

        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.CircleCast(pos, 0.75f, direction, 0.2f, mask);
        return hit.collider == null;
    }

    public static void Move(GameObject entity, Vector3 direction, float speed)
{
    float frameDistance = speed * Time.deltaTime;

    // cast specific distance away
    RaycastHit2D hit = Physics2D.CircleCast(entity.transform.position, 0.75f, direction, frameDistance, LayerMask.GetMask("Wall"));

    // if we meet a wakk
    if (hit.collider != null)
    {
        // clamp the distance
        frameDistance = Mathf.Max(0f, hit.distance - 0.05f);
    }

    entity.transform.position += direction * frameDistance;
    SnapToAxis(entity, direction);
}

    public static void SnapToAxis(GameObject entity, Vector3 direction)
    {
        Vector3 pos = entity.transform.position;
        // Якщо рухаємось по X, вирівнюємо Y до найближчого центру клітинки
        if (Mathf.Abs(direction.x) > 0)
        {
            pos.y = Mathf.Round(pos.y / GridStep) * GridStep;
        }
        // Якщо рухаємось по Y, вирівнюємо X
        else if (Mathf.Abs(direction.y) > 0)
        {
            pos.x = Mathf.Round(pos.x / GridStep) * GridStep;
        }
        entity.transform.position = pos;
    }

    public static void ChangeRot(GameObject entity, Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        entity.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public static Vector3 GetDirection(Vector2 rawDir)
    {
        if (Mathf.Abs(rawDir.x) > Mathf.Abs(rawDir.y))
            return new Vector3(rawDir.x > 0 ? 1 : -1, 0, 0);
        else
            return new Vector3(0, rawDir.y > 0 ? 1 : -1, 0);
    }
}