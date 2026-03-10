using UnityEngine;

public class UniversalBody : MonoBehaviour
{
    [HideInInspector]public Weapons weapons;

    public StatsTemplate StatsTemplate;
    public Stats stats;
    public bool spedUp;
    void Awake()
    {
        weapons = GetComponent<Weapons>(); 
        stats = new Stats(StatsTemplate);
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
    public void MainDamageUpgrade()
    {
        weapons.mainWeapon.DamageUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("m");
    }
    public void MainSpeedUpgrade()
    {
        weapons.mainWeapon.SpeedUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("m");
    }
    public void MainSizeUpgrade()
    {
        weapons.mainWeapon.SizeUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("m");
    }
    public void MainRechargeUpgrade()
    {
        weapons.mainWeapon.RechargeUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("m");
    }
    public void SecondaryDamageUpgrade()
    {           
        weapons.secondaryWeapon.DamageUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("s");
    }           
    public void SecondarySpeedUpgrade()
    {           
        weapons.secondaryWeapon.SpeedUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("s");
    }           
    public void SecondarySizeUpgrade()
    {           
        weapons.secondaryWeapon.SizeUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("s");
    }           
    public void SecondaryRechargeUpgrade()
    {           
        weapons.secondaryWeapon.RechargeUpgrade();
        ScoreManager.CloseMenu();
        Debug.Log("s");
    }
    public void HealthUpgrade()
    {
        stats.health += stats.health / 10;
        ScoreManager.CloseMenu();
        Debug.Log("h");
    }
    public void SpeedUpgrade()
    {
        stats.speed += stats.speed / 5;
        ScoreManager.CloseMenu();
        Debug.Log("h");
    }
}
