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
        if (stats.coolDown > 0) return;

        if (user == null || userStats == null) return;

        user.StartCoroutine(DashTimer(userStats, user)); //calling for speed increase 

        user.dashParticle.Play();

        RaycastHit2D[] dashHit = Physics2D.CircleCastAll(new Vector3(user.gameObject.transform.position.x, user.gameObject.transform.localPosition.y), 
                                                        stats.size,
                                                        userStats.currentDirection, 
                                                        userStats.movementSpeed * 0.2f, 
                                                        LayerMask.GetMask(userStats.tag));
        foreach (var hit in dashHit)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage); 
        }
        stats.coolDown = stats.reChargeTime;
    }
    public IEnumerator DashTimer(Stats userStats, Weapons user)
    {
        if (user.gameObject.tag == "Enemy")
        {
            user.gameObject.GetComponent<EnemyAI>().followPlayer = true;
        }

        userStats.movementSpeed = userStats.movementSpeed * stats.speed * 5;
        user.gameObject.GetComponent<HealthSys>().Immunity(0.2f);
        user.gameObject.GetComponent<UniversalBody>().spedUp = true;
        //Play animation

        yield return new WaitForSeconds(0.1f);

        userStats.movementSpeed = userStats.movementSpeed / stats.speed / 5;

        //exit animation

        yield return new WaitForSeconds(0.5f);
        user.gameObject.GetComponent<UniversalBody>().spedUp = false;

        if (user.gameObject.tag == "Enemy")
        {
            user.gameObject.GetComponent<EnemyAI>().followPlayer = false;
        }

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
        stats.reChargeTime -= stats.reChargeTime / 10f;
    }
    public override void SizeUpgrade()
    {
        stats.size += 0.2f;
    }
}
