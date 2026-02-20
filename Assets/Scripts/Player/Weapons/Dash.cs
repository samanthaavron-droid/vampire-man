using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Dash : Weapon
{
    public Dash(StatsTemplate stats)
    {
        base.stats = new Stats(stats);
    }
    public override void Use(PlayerBase _playerBase)
    {
        if (Time.time < stats.coolDown)
        {
            Debug.Log("Dash on cooldown");
            return;
        }

        stats.coolDown = Time.time + stats.reChargeTime;

        _playerBase.StartCoroutine(DashTimer(_playerBase)); //calling for speed increase and no collision with enemies

        RaycastHit2D[] dashHit = Physics2D.CircleCastAll(_playerBase.transform.position, 0.5f, _playerBase._movementController._currentDirection, (_playerBase._movementController._currentDirection * _playerBase.speed).magnitude, LayerMask.GetMask("Enemy"));
        //Debug.DrawRay(transform.position, (_movementController._currentDirection * _playerBase.speed) * 0.05f, Color.red, 1f);

        foreach (var hit in dashHit)
        {
            hit.collider.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
            Debug.Log("Dash Damage");
        }

        Debug.Log("Dash performed");
    }
    public IEnumerator DashTimer(PlayerBase _playerBase)
    {
        Physics2D.IgnoreLayerCollision(0, 6, true);
        _playerBase.speed = _playerBase.speed * stats.speed * 10;
        //Debug.Log("speed increased");

        yield return new WaitForSeconds(0.05f);

        Physics2D.IgnoreLayerCollision(0, 6, false);
        _playerBase.speed = _playerBase.speed / stats.speed / 10;
        //Debug.Log("speed descreased");
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
