using UnityEngine;
using UnityEngine.InputSystem;
using static Weapons;

public class PlayerBase : MonoBehaviour
{
    private MovementController _movementController => GetComponent<MovementController>();
    private Weapons _weapons => GetComponent<Weapons>();

    public InputActionReference attack;

    public int health;
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
        _movementController.movementDirection = -_movementController.movementDirection;
    }
    public void MainAttack(InputAction.CallbackContext obj)
    {
        Debug.Log("main attack called");
        _weapons.AttackMain();
    }
    private void OnEnable()
    {
        attack.action.performed += MainAttack;
    }
}
