using UnityEngine;

public class UniversalBody : MonoBehaviour
{
    public Weapons weapons => GetComponent<Weapons>();

    public StatsTemplate StatsTemplate;
    public Stats stats;

    void Awake()
    {
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
}
