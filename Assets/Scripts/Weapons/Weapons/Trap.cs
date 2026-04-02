using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Trap : Weapon
{
    GameObject trap;
    public Trap(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons weapons, Stats plstats)
    {
        if (stats.coolDown > 0) return;

        if (weapons == null || plstats == null) return;

        trap = GameObject.Instantiate(weapons.trap, weapons.transform.position, Quaternion.identity);

        trap.GetComponent<TrapBody>().SetStats(stats, weapons);

        stats.coolDown = stats.reChargeTime;
    }
    public override void SpeedUpgrade()
    {
        stats.speed += 0.5f;
    }
    public override void DamageUpgrade()
    {
        stats.damage += 2f;
    }
    public override void RechargeUpgrade()
    {
        stats.reChargeTime -= stats.reChargeTime / 10f;
    }
}
