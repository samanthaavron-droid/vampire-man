using UnityEngine;

public static class MovementSys
{
    public static void MoveTo(GameObject entity, Vector2 direction, float speed, ref Vector3 lastDir)
    {
        if (RayCastCheck(entity, direction))
        {
            entity.transform.position += new Vector3(direction.x, direction.y, 0f) * speed * Time.deltaTime;
            lastDir = new Vector3(direction.x, direction.y, 0f);
            RotateDirection(entity, direction);
        } 
        else
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
}