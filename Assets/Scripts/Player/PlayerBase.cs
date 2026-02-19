using UnityEngine;
using UnityEngine.InputSystem;
using static Weapons;

public class PlayerBase : MonoBehaviour
{
    [HideInInspector] public MovementController _movementController => GetComponent<MovementController>();
    private Weapons _weapons => GetComponent<Weapons>();

    public InputActionReference attack;
    public InputActionReference upgradeDebug;

    public float health;
    public float speed;

    private bool immune;
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
    public void UpgradeDebug(InputAction.CallbackContext obj)
    {
        _weapons.mainWeapon.SpeedUpgrade();
    }
    private void OnEnable()
    {
        attack.action.performed += MainAttack;
        upgradeDebug.action.performed += UpgradeDebug;
    }
}
