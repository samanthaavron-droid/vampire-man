using System.Collections;
using UnityEngine;

public class UniversalBody : MonoBehaviour
{
    [HideInInspector]public Weapons weapons;

    public StatsTemplate StatsTemplate;
    public Stats stats;
    public bool spedUp;
    private HealthSys healthSys;
    void Awake()
    {
        weapons = GetComponent<Weapons>();
        healthSys = GetComponent<HealthSys>();
        stats = new Stats(StatsTemplate);
    }
    void Update()
    {
        if (weapons.mWeapon != WeaponChoice.None)
        {
            if (weapons.mainWeapon.stats.coolDown > 0)
            {
                weapons.mainWeapon.stats.coolDown -= Time.deltaTime;
            }
        }
        if (weapons.sWeapon != WeaponChoice.None)
        {
            if (weapons.secondaryWeapon.stats.coolDown > 0)
            {
                weapons.secondaryWeapon.stats.coolDown -= Time.deltaTime;
            }
        }
    }
    public void MainAttack()
    {
        if (weapons.mainWeapon != null)
        {
            weapons.mainWeapon.Use(weapons, stats);
        }
    }
    public void SecondaryAttack()
    {
        if (weapons.secondaryWeapon != null)
        {
            weapons.secondaryWeapon.Use(weapons, stats);
        }
    }
    public IEnumerator Stun(float power, float duration)
    {
        stats.movementSpeed = stats.movementSpeed / power;

        yield return new WaitForSeconds(duration);

        stats.movementSpeed = stats.movementSpeed * power;
    }
    public void MainDamageUpgrade()
    {
        weapons.mainWeapon.DamageUpgrade();
        ScoreManager.CloseMenu();
    }
    public void MainSpeedUpgrade()
    {
        weapons.mainWeapon.SpeedUpgrade();
        ScoreManager.CloseMenu();
    }
    public void MainSizeUpgrade()
    {
        weapons.mainWeapon.SizeUpgrade();
        ScoreManager.CloseMenu();
    }
    public void MainRechargeUpgrade()
    {
        weapons.mainWeapon.RechargeUpgrade();
        ScoreManager.CloseMenu();
    }
    public void SecondaryDamageUpgrade()
    {           
        weapons.secondaryWeapon.DamageUpgrade();
        ScoreManager.CloseMenu();
    }           
    public void SecondarySpeedUpgrade()
    {           
        weapons.secondaryWeapon.SpeedUpgrade();
        ScoreManager.CloseMenu();
    }           
    public void SecondarySizeUpgrade()
    {           
        weapons.secondaryWeapon.SizeUpgrade();
        ScoreManager.CloseMenu();
    }           
    public void SecondaryRechargeUpgrade()
    {           
        weapons.secondaryWeapon.RechargeUpgrade();
        ScoreManager.CloseMenu();
    }
    public void HealthUpgrade()
    {
        stats.health += stats.health / 10;
        healthSys.startHealth += healthSys.startHealth / 10;
        ScoreManager.CloseMenu();
    }
    public void SpeedUpgrade()
    {
        stats.speed += stats.speed / 5;
        ScoreManager.CloseMenu();
    }
}
