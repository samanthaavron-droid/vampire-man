using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Whip : Weapon
{
    public Whip(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons user, Stats userStats)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Whip on cooldown by " + user.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        RaycastHit2D[] whip = Physics2D.CircleCastAll(user.gameObject.transform.position,
                                                        0.5f,
                                                        userStats.currentDirection,
                                                        stats.size,
                                                        LayerMask.GetMask(stats.tag));
        foreach (var hit in whip)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
            user.StartCoroutine(Stun(hit.collider.gameObject.GetComponent<UniversalBody>()));
        }
    }
    public IEnumerator Stun(UniversalBody target)
    {
        target.stats.movementSpeed = target.stats.movementSpeed / 10;
        //animation play

        yield return new WaitForSeconds(stats.speed);

        target.stats.movementSpeed = target.stats.movementSpeed * 10;
    }
    public override void SpeedUpgrade()
    {
        stats.speed += 0.2f;
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
        stats.size += 1f;
    }
}
