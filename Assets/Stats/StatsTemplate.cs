using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class StatsTemplate : ScriptableObject
{
    public float damage;
    public float speed;
    public float reChargeTime;
    public float size;
    public float health;
    public string tag;
    public float movementSpeed;
    [HideInInspector] public Vector2 currentDirection;
}
