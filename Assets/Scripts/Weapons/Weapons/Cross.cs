using UnityEngine;
using System.Collections;

public class Cross : Weapon
{
    public Cross(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons user, Stats userStats)
    {
        if (stats.coolDown > 0) return;

        if (user == null || userStats == null) return;

        GameObject prefab = GameObject.Instantiate(user.crossAnim, user.transform.position, Quaternion.identity);
        prefab.transform.transform.localScale = new Vector2(stats.size * 0.75f, stats.size * 0.75f);

        Collider2D[] crossHit = Physics2D.OverlapCircleAll(user.gameObject.transform.position, 
                                                            stats.size,
                                                            LayerMask.GetMask("Enemy"));
        foreach (var hit in crossHit)
        {
            user.StartCoroutine(hit.gameObject.GetComponent<UniversalBody>().Stun(100, stats.speed));
            hit.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
        }

        user.gameObject.GetComponent<HealthSys>()._impulseSource.GenerateImpulse(1f);

        stats.coolDown = stats.reChargeTime;
    }
    public override void SpeedUpgrade()
    {
        stats.speed += 1f;
    }
    public override void DamageUpgrade()
    {
        stats.damage += 2f;
    }
    public override void RechargeUpgrade()
    {
        stats.reChargeTime -= stats.reChargeTime / 10f;
    }
    public override void SizeUpgrade()
    {
        stats.size += 1f;
    }
}
