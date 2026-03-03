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
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Cross on cooldown by " + user.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        Collider2D[] crossHit = Physics2D.OverlapCircleAll(user.gameObject.transform.position, 
                                                            stats.size,
                                                            LayerMask.GetMask("Enemy"));
        foreach (var hit in crossHit)
        {
            user.StartCoroutine(SuperStun(hit));
        }
    }
    public IEnumerator SuperStun(Collider2D target)
    {
        target.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);

        float tempSpeed = target.gameObject.GetComponent<UniversalBody>().stats.movementSpeed;
        target.gameObject.GetComponent<UniversalBody>().stats.movementSpeed = 0.1f;

        yield return new WaitForSeconds(stats.speed);

        target.gameObject.GetComponent<UniversalBody>().stats.movementSpeed = tempSpeed;
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
        stats.reChargeTime += -stats.reChargeTime / 10f;
    }
    public override void SizeUpgrade()
    {
        stats.size += 1f;
    }
}
