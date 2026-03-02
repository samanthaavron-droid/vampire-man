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
        weapons.mainWeapon.Use(weapons, stats);
    }
    public void SecondaryAttack()
    {
        weapons.secondaryWeapon.Use(weapons, stats);
    }
}
