using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Weapons : MonoBehaviour
{   
    public enum WeaponType
    {
        None,
        Dash
    }
    public enum StatType
    {
        speed,
        damage,
        reChargeTime
    }
    public WeaponType mainWeapon;
    public WeaponType secondaryWeapon;

    private PlayerBase _playerBase => GetComponent<PlayerBase>();
    private MovementController _movementController => GetComponent<MovementController>();

    private LayerMask mask;

    Stats dashLevel = new Stats();
    Stats swordLevel = new Stats();
    void Start()
    {
        mask = LayerMask.GetMask("Enemy");
    }
    void Update()
    {
        
    }
    public void UpgradeWeapon(string weaponType, StatType statToUpgrade, float level)
    {
        switch (weaponType)
        {
            case "Dash":
                switch (statToUpgrade)
                {
                    case StatType.speed:

                        break;
                }
                break;
        }
    }
    public void AttackMain()
    {
        switch (mainWeapon)
        {
            case WeaponType.None:
                Debug.Log("No weapons available");
                break;

            case WeaponType.Dash:
                Dash();
                break;

            default:
                Debug.LogWarning("weapon type not recognized!");
                break;
        }
    }
    public void Dash()
    {
        if (Time.time < dashLevel.coolDown)
        {
            Debug.Log("Dash on cooldown");
            return;
        }

        dashLevel.coolDown = Time.time + dashLevel.reChargeTime;

        StartCoroutine(DashTimer()); //calling for speed increase and no collision with enemies

        RaycastHit2D dashHit = Physics2D.CircleCast(gameObject.transform.position, 0.5f, _movementController._currentDirection, (_movementController._currentDirection * _playerBase.speed).magnitude, mask);
        //Debug.DrawRay(transform.position, (_movementController._currentDirection * _playerBase.speed) * 0.05f, Color.red, 1f);

        if (dashHit)
        {
            dashHit.collider.gameObject.GetComponent<EnemyBase>().TakeDamage(dashLevel.damage);
        }

        Debug.Log("Dash performed");
    }
    public IEnumerator DashTimer()
    {
        Physics2D.IgnoreLayerCollision(0, 6, true);
        _playerBase.speed = _playerBase.speed * dashLevel.speed * 10;
        //Debug.Log("speed increased");

        yield return new WaitForSeconds(0.05f);

        Physics2D.IgnoreLayerCollision(0, 6, false);
        _playerBase.speed = _playerBase.speed / dashLevel.speed / 10;
        //Debug.Log("speed descreased");
    }
}
public class Stats
{
    public float speed = 1f;
    public float damage = 1f;
    public float reChargeTime = 1f;
    public float coolDown = 1f;
}