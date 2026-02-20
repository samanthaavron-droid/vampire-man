using UnityEngine;
using UnityEngine.InputSystem;
using static Weapons;

public class PlayerBase : MonoBehaviour
{
    [HideInInspector] public MovementController _movementController => GetComponent<MovementController>();
    public Weapons _weapons => GetComponent<Weapons>();

    public InputActionReference attack;
    public InputActionReference secondAttack;
    public InputActionReference upgradeDebug;

    public float health;
    public float speed;

    private bool immune = false;
    void Start()
    {

    }
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
        }
    }
    public void Damage(int damage)
    {
        //damage from enemy
        health -= damage;

        //knockback
        //_movementController.movementDirection = -_movementController.movementDirection;
    }
    public void TakeDamage(float damage)
    {
        if (immune) //immunity check
        {
            return;
        }

        health -= damage; 
    }
    public void MainAttack(InputAction.CallbackContext obj)
    {
        _weapons.mainWeapon.Use(this);
    }
    public void SecondaryAttack(InputAction.CallbackContext obj)
    {
        _weapons.secondaryWeapon.Use(this);
    }
    public void UpgradeDebug(InputAction.CallbackContext obj)
    {
        _weapons.mainWeapon.SpeedUpgrade();
    }
    private void OnEnable()
    {
        attack.action.performed += MainAttack;
        secondAttack.action.performed += SecondaryAttack;

        upgradeDebug.action.performed += UpgradeDebug;
    }
}
