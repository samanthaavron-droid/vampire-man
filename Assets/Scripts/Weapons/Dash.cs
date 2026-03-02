using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Dash : Weapon
{
    public Dash(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons weapons, Stats plstats)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Dash on cooldown by" + weapons.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        weapons.StartCoroutine(DashTimer(plstats)); //calling for speed increase and no collision with enemies

        RaycastHit2D[] dashHit = Physics2D.CircleCastAll(weapons.gameObject.transform.position, 
                                                        0.5f,
                                                        plstats.currentDirection, 
                                                        (plstats.currentDirection * stats.speed).magnitude, 
                                                        LayerMask.GetMask(plstats.tag));
        foreach (var hit in dashHit)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);        }

        Debug.Log("Dash performed by " + weapons.gameObject.name);
    }
    public IEnumerator DashTimer(Stats plstats)
    {
        plstats.movementSpeed = plstats.movementSpeed * stats.speed * 10;        
        //Play animation

        yield return new WaitForSeconds(0.05f);

        plstats.movementSpeed = plstats.movementSpeed / stats.speed / 10;
        //exit animation
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
        stats.reChargeTime += stats.reChargeTime / 10f;
    }
}
