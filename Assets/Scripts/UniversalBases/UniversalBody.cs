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
}
