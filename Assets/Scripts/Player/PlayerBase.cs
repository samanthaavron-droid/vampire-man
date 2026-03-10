using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using static Weapons;

public class PlayerBase : MonoBehaviour
{
    private UniversalBody _body;
    [HideInInspector] public MovementController _movementController;

    public InputActionReference attack;
    public InputActionReference secondAttack;

    public GameObject weaponChoiceUI;
    public GameObject firstButtonWeapon;

    private PlayerInput playerInput;

    public static bool dead;
    public bool chooseWeapon;
    private void Start()
    {
        _movementController = GetComponent<MovementController>(); 
        _body = GetComponent<UniversalBody>();
        playerInput = GetComponent<PlayerInput>();

        dead = false;

        weaponChoiceUI.SetActive(false);

        if (chooseWeapon)
            StartWeapon();
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
    private void StartWeapon()
    {
        Time.timeScale = 0f;

        weaponChoiceUI.SetActive(true);
        playerInput.SwitchCurrentActionMap("UI"); //switching input

        EventSystem.current.SetSelectedGameObject(firstButtonWeapon);
    }
    private void ExitStartWeaponMenu()
    {
        Time.timeScale = 1f;
        weaponChoiceUI.SetActive(false);
        playerInput.SwitchCurrentActionMap("playerController"); //switching input
    }
    public void SwordChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Sword;
        _body.weapons.SetWeapon();
        ExitStartWeaponMenu();
    }
    public void DashChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Dash;
        _body.weapons.SetWeapon();
        ExitStartWeaponMenu();
    }
    public void WhipChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Whip;
        _body.weapons.SetWeapon();
        ExitStartWeaponMenu();
    }
}
