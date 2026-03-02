using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Weapons : MonoBehaviour
{
    public WeaponChoice mWeapon;
    public WeaponChoice sWeapon;

    public Weapon mainWeapon;
    public Weapon secondaryWeapon;

    public StatsTemplate dashStats;
    public StatsTemplate swordStats;
    public StatsTemplate trapStats;

    public Collider2D sword;
    public GameObject trap;
    private void OnValidate()
    {
        SetWeapon();
    }
    void Awake()
    {
        SetWeapon();
    }
    public void SetWeapon() {
        switch (mWeapon)
        {
            case WeaponChoice.Sword:
                mainWeapon = new Sword(swordStats);
                break;
            case WeaponChoice.Dash:
                mainWeapon = new Dash(dashStats);
                break;
            case WeaponChoice.Trap:
                mainWeapon = new Trap(trapStats);
                break;
                /*
                case WeaponChoice.Cross:
                    mainWeapon = new Cross(crossStats);
                    break;
                case WeaponChoice.Whip:
                    mainWeapon = new Whip(whipStats);
                    break;
                */
        }
        switch (sWeapon)
        {
            case WeaponChoice.Sword:
                secondaryWeapon = new Sword(swordStats);
                break;
            case WeaponChoice.Dash:
                secondaryWeapon = new Dash(dashStats);
                break;
            case WeaponChoice.Trap:
                secondaryWeapon = new Trap(trapStats);
                break;
                /*
            case WeaponChoice.Cross:
                secondaryWeapon = new Cross(crossStats);
                break;
            case WeaponChoice.Whip:
                secondaryWeapon = new Whip(whipStats);
                break;
            */
        }
    }
}
public enum WeaponChoice
{
    Sword,
    Dash,
    Trap,
    Cross,
    Whip,
};