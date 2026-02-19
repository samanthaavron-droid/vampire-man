using UnityEngine;

public class Stats : IStats
{
    public float damage { get; set; }
    public float speed { get; set; }
    public float coolDown { get; set; }
    public float reChargeTime { get; set; }
    public Stats(StatsTemplate s)
    {
        damage = s.damage;
        speed = s.speed;
        reChargeTime = s.reChargeTime;
    }
}
