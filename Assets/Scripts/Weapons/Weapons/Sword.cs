using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Sword : Weapon
{
    Collider2D deathZone;
    public Sword (StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }

    public override void Use(Weapons user, Stats userStats)
    {
        if (Time.time < stats.coolDown)
        {
            //Debug.Log("Sword on cooldown by " + user.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        deathZone = GameObject.Instantiate(user.sword, user.transform.position, Quaternion.identity);
        deathZone.transform.localScale = new Vector3(stats.size, stats.size, 1f);
        deathZone.transform.SetParent(user.gameObject.transform);

        deathZone.GetComponent<SwordBody>().SetStats(stats, user);
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
    public override void SizeUpgrade()
    {
        stats.size += stats.size / 10f;
    }
}
