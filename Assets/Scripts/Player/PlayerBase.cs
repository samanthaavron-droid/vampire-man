using UnityEngine;
using UnityEngine.InputSystem;
using static Weapons;

public class PlayerBase : MonoBehaviour
{
    private UniversalBody _body => GetComponent<UniversalBody>();
    [HideInInspector] public MovementController _movementController => GetComponent<MovementController>();

    public InputActionReference attack;
    public InputActionReference secondAttack;

    public static bool dead;
    private void Start()
    {
        dead = false;
    }
    public void MainAttack(InputAction.CallbackContext obj)
    {
        _body.MainAttack();
    }
    public void SecondaryAttack(InputAction.CallbackContext obj)
    {
        _body.SecondaryAttack();
    }
    private void OnEnable()
    {
        attack.action.performed += MainAttack;
        secondAttack.action.performed += SecondaryAttack;
    }
}
