using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Weapons : MonoBehaviour
{
    public Weapon mainWeapon;
    public Weapon secondaryWeapon;

    public StatsTemplate dashMainStats;
    public StatsTemplate swordStats;

    public Collider2D damageZone;
    void Start()
    {
        mainWeapon = new Sword(swordStats);
        secondaryWeapon = new Dash(dashMainStats);
    }
}