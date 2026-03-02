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

    public override void Use(Weapons weapons, Stats plstats)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Sword on cooldown by " + weapons.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        weapons.StartCoroutine(SwordAttack(stats));
        deathZone = GameObject.Instantiate(weapons.sword, weapons.transform.position, Quaternion.identity, weapons.transform);
        deathZone.transform.localScale = new Vector3(stats.size, stats.size, 1f);

        if (deathZone.GetComponent<Collider2D>() != null)
        {
            Collider2D zoneCollider = deathZone.GetComponent<Collider2D>();
            List<Collider2D> hitEnemy = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();

            filter.SetLayerMask(LayerMask.GetMask(plstats.tag));
            filter.useLayerMask = true;

            zoneCollider.Overlap(filter, hitEnemy);

            foreach (Collider2D enemy in hitEnemy)
            {
                enemy.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
            }
        }
    }
    public IEnumerator SwordAttack(Stats stats)
    {
        yield return new WaitForSeconds(stats.speed);

        GameObject.Destroy(deathZone.gameObject);
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
