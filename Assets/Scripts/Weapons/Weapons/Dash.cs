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

        user.StartCoroutine(DashTimer(userStats, user)); //calling for speed increase 

        RaycastHit2D[] dashHit = Physics2D.CircleCastAll(new Vector3(user.gameObject.transform.position.x, user.gameObject.transform.localPosition.y - 0.5f), 
                                                        stats.size,
                                                        userStats.currentDirection, 
                                                        userStats.movementSpeed * 0.06f, 
                                                        LayerMask.GetMask(userStats.tag));
        foreach (var hit in dashHit)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage); 
        }
    }
    public IEnumerator DashTimer(Stats userStats, Weapons user)
    {
        userStats.movementSpeed = userStats.movementSpeed * stats.speed * 10;
        user.gameObject.GetComponent<HealthSys>().Immunity(0.2f);
        user.gameObject.GetComponent<UniversalBody>().spedUp = true;
        //Play animation

        yield return new WaitForSeconds(0.05f);

        userStats.movementSpeed = userStats.movementSpeed / stats.speed / 10;

        //exit animation

        yield return new WaitForSeconds(0.5f);
        user.gameObject.GetComponent<UniversalBody>().spedUp = false;
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
        stats.size += 0.2f;
    }
}
