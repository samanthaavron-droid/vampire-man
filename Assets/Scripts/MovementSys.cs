using UnityEngine;

public static class MovementSys
{
    public static void MoveTo(GameObject entity, Vector2 direction, float speed, ref Vector3 lastDir)
    {
        Vector2 checkedDir = GetDirection(direction);

        if (RayCastCheck(entity, checkedDir))
        {
            entity.transform.position += new Vector3(checkedDir.x, checkedDir.y, 0f) * speed * Time.deltaTime;
            lastDir = new Vector3(checkedDir.x, checkedDir.y, 0f);
            RotateDirection(entity, checkedDir);
        } else
        {
            entity.transform.position += lastDir * speed * Time.deltaTime;
        }
    }
    public static void RotateDirection(GameObject entity, Vector2 direction)
    {
        entity.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
    public static bool RayCastCheck(GameObject entity, Vector2 direction)
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.CircleCast(entity.transform.position, 0.3f, direction, 1f, mask);

        if (hit)
        {
            return false;
        } 
        else
        {
            return true;
        }
    }
    public static Vector2 GetDirection(Vector2 rawDir)
    {
        if (rawDir.x != 0f)
        {
            return new Vector2(rawDir.x, 0f);
        } else
        {
            return new Vector2(0f, rawDir.y);
        }
    }
}