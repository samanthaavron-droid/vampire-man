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
        if (stats.coolDown > 0) return;

        if (user == null || userStats == null) return;

        if (user.weaponAnim != null)
        {
            user.weaponAnim.gameObject.transform.localScale = new Vector2(stats.size * 0.44f, 1f);
            user.weaponAnim.transform.right = userStats.currentDirection;
            user.weaponAnim.SetTrigger("whip");
        }

        

        RaycastHit2D[] whip = null;

        whip = Physics2D.CircleCastAll(user.gameObject.transform.position,
                                                                0.5f,
                                                                userStats.currentDirection,
                                                                stats.size,
                                                                LayerMask.GetMask(stats.tag));

        if (whip == null) return;

        foreach (var hit in whip)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);

            if (hit.collider.gameObject.GetComponent<UniversalBody>().spedUp == false)
            {
                user.StartCoroutine(hit.collider.gameObject.GetComponent<UniversalBody>().Stun(10, stats.speed));
            } 
        }
        stats.coolDown = stats.reChargeTime;
    }
    public override void SpeedUpgrade()
    {
        stats.speed += 0.1f;
    }
    public override void DamageUpgrade()
    {
        stats.damage += 1f;
    }
    public override void RechargeUpgrade()
    {
        stats.reChargeTime -= stats.reChargeTime / 10f;
    }
    public override void SizeUpgrade()
    {
        stats.size += 0.5f;
    }
}
