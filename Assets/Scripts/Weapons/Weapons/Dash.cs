using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Dash : Weapon
{
    public Dash(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(Weapons user, Stats userStats)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Dash on cooldown by " + user.gameObject.name);
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        user.StartCoroutine(DashTimer(userStats)); //calling for speed increase

        RaycastHit2D[] dashHit = Physics2D.CircleCastAll(user.gameObject.transform.position, 
                                                        0.5f,
                                                        userStats.currentDirection, 
                                                        (userStats.currentDirection * stats.speed).magnitude, 
                                                        LayerMask.GetMask(userStats.tag));
        foreach (var hit in dashHit)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);        
        }
    }
    public IEnumerator DashTimer(Stats userStats)
    {
        userStats.movementSpeed = userStats.movementSpeed * stats.speed * 10;        
        //Play animation

        yield return new WaitForSeconds(0.05f);

        userStats.movementSpeed = userStats.movementSpeed / stats.speed / 10;
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
        stats.reChargeTime += -stats.reChargeTime / 10f;
    }
}
