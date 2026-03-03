using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : Weapon
{
    GameObject trap;
    public Trap(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons weapons, Stats plstats)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Trap on cooldown by " + weapons.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        trap = GameObject.Instantiate(weapons.trap, weapons.transform.position, Quaternion.identity);

        trap.GetComponent<TrapBody>().SetStats(stats, weapons);
    }
    public override void SpeedUpgrade()
    {
        stats.speed += 0.5f;
    }
    public override void DamageUpgrade()
    {
        stats.damage += 1f;
    }
    public override void RechargeUpgrade()
    {
        stats.reChargeTime += -stats.reChargeTime / 10f;
    }
}
