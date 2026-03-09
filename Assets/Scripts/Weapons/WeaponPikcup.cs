using UnityEngine;

public class WeaponPikcup : MonoBehaviour
{
    public WeaponChoice weapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<UniversalBody>().weapons.sWeapon = weapon;
            GetComponent<UniversalBody>().weapons.SetWeapon();
        }
    }
}