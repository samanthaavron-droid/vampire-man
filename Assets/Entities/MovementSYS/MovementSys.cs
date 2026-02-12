using UnityEngine;

public static class MovementSys
{
    public static void MoveTo(GameObject entity, Vector2 direction, float speed)
    {
        entity.transform.position += new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;
    }
}