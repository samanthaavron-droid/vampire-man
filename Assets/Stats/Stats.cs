using UnityEngine;

public class Stats
{
    public float damage { get; set; }
    public float speed { get; set; }
    public float coolDown { get; set; }
    public float reChargeTime { get; set; }
    public float size { get; set; }
    public float health { get; set; }
    public float movementSpeed { get; set; }
    public Vector2 currentDirection { get; set; }
    public string tag { get; set; }
    public int xp { get; set; }
    public Stats(StatsTemplate s)
    {
        damage = s.damage;
        speed = s.speed;
        reChargeTime = s.reChargeTime;
        size = s.size;
        health = s.health;
        movementSpeed = s.movementSpeed;
        currentDirection = s.currentDirection;
        tag = s.tag;
        xp = s.xp;
    }
}
