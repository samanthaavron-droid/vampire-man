using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Weapons : MonoBehaviour
{
    public Weapon mainWeapon;
    public Weapon secondaryWeapon;

    public StatsTemplate dashMainStats;
    public StatsTemplate swordStats;
    void Start()
    {
        mainWeapon = new Dash(dashMainStats);
    }
}