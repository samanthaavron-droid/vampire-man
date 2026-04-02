using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerBase : MonoBehaviour
{
    private UniversalBody _body;
    [HideInInspector] public MovementController _movementController;
    private HealthSys _healthSys;
    private Weapons _weapons;

    public InputActionReference attack;
    public InputActionReference secondAttack;

    public GameObject weaponChoiceUI;
    public GameObject firstButtonWeapon;

    public Image healthBar;
    public GameObject[] coolDowns;

    public GameObject difficultySet;
    public bool tutorial;

    private TextMeshProUGUI[] coolDownNames;
    private Image[] coolDownImages;

    private PlayerInput playerInput;

    public static bool dead { get; set;  }
    public bool chooseWeapon;

    public Explanation tutorialExp;

    private bool _secondWeapon;

    private Vector2 currentDir;
    private Animator animator;
    private void Start()
    {
        _movementController = GetComponent<MovementController>(); 
        _body = GetComponent<UniversalBody>();
        playerInput = GetComponent<PlayerInput>();
        _healthSys = GetComponent<HealthSys>();
        _weapons = GetComponent<Weapons>();
        animator = GetComponentInChildren<Animator>();

        coolDownImages = new Image[coolDowns.Length];
        coolDownNames = new TextMeshProUGUI[coolDowns.Length];

        for (int i = 0; i < coolDowns.Length; i++)
        {
            coolDownImages[i] = coolDowns[i].GetComponentInChildren<Image>();
            coolDownNames[i] = coolDowns[i].GetComponentInChildren<TextMeshProUGUI>();
            coolDownImages[i].gameObject.SetActive(false);
            coolDownNames[i].gameObject.SetActive(false);
        }

        if (difficultySet != null)
            difficultySet.gameObject.SetActive(false);
        
        weaponChoiceUI.SetActive(false);

        dead = false;

        if (tutorial == true)
        {
            chooseWeapon = false;
            tutorialExp.StartTutorial();
        }

        if (chooseWeapon == true)
        {
            chooseWeapon = false;
            StartWeapon();
        }

        _secondWeapon = false;
    }
    void Update()
    {
        if (currentDir != _body.stats.currentDirection)
        {
            AnimationUpdate();
        }

        if (chooseWeapon == true)
        {
            chooseWeapon = false;
            StartWeapon();
        }

        healthBar.fillAmount = (_body.stats.health / _healthSys.startHealth);

        if (_weapons.mWeapon != WeaponChoice.None)
        {
            coolDownImages[0].fillAmount = (_weapons.mainWeapon.stats.coolDown / _weapons.mainWeapon.stats.reChargeTime);

        }
        if (_weapons.sWeapon != WeaponChoice.None)
        {
            coolDownImages[1].fillAmount = (_weapons.secondaryWeapon.stats.coolDown / _weapons.secondaryWeapon.stats.reChargeTime);
        }

        if (_weapons.sWeapon != WeaponChoice.None && _secondWeapon == false)
        {
            _secondWeapon = true;
            UpgradeSecondWeapon();
        }
        if (dead == true)
        {
            healthBar.transform.parent.gameObject.SetActive(false);
            coolDowns[0].gameObject.SetActive(false);
            coolDowns[1].gameObject.SetActive(false);
        }
    }
    private void AnimationUpdate()
    {
        currentDir = _body.stats.currentDirection;

        if (_body.stats.currentDirection.x > 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("right");
        else if (_body.stats.currentDirection.x < 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("left");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y > 0)
            animator.SetTrigger("up");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y < 0)
            animator.SetTrigger("down");

    }
    public void SetWeaponRechargeUI()
    {
        coolDownNames[0].text = _weapons.mWeapon.ToString();
        coolDownNames[0].gameObject.SetActive(true);
        coolDownImages[0].gameObject.SetActive(true);

        if (_weapons.sWeapon == WeaponChoice.None)
        {
            coolDownNames[1].gameObject.SetActive(false);
            coolDownImages[1].gameObject.SetActive(false);
        } else
        {
            coolDownNames[1].text = _weapons.sWeapon.ToString();
            coolDownImages[1].gameObject.SetActive(true);
            coolDownNames[1].gameObject.SetActive(true);
        }
    }
    private void UpgradeSecondWeapon()
    {
        _weapons.secondaryWeapon.stats.damage = _weapons.secondaryWeapon.stats.damage * 2;
        _weapons.secondaryWeapon.stats.speed = _weapons.secondaryWeapon.stats.speed * 2;
        _weapons.secondaryWeapon.stats.size = _weapons.secondaryWeapon.stats.size * 2;
        _weapons.secondaryWeapon.stats.reChargeTime = _weapons.secondaryWeapon.stats.reChargeTime * 2;
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
    public void ExitStartWeaponMenu()
    {
        SetWeaponRechargeUI();

        if(tutorial == false)
            difficultySet.gameObject.SetActive(false);

        Time.timeScale = 1f;
        playerInput.SwitchCurrentActionMap("playerController"); //switching input
    }
    public void SwordChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Sword;
        _body.weapons.SetWeapon();
        
        if (tutorial == false)
            SetDifficulty();
        else
        {
            weaponChoiceUI.SetActive(false);
            ExitStartWeaponMenu();
        }
    }
    public void DashChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Dash;
        _body.weapons.SetWeapon();

        if (tutorial == false)
            SetDifficulty();
        else
        {
            weaponChoiceUI.SetActive(false);
            ExitStartWeaponMenu();
        }
    }
    public void WhipChoice()
    {
        _body.weapons.mWeapon = WeaponChoice.Whip;
        _body.weapons.SetWeapon();

        if (tutorial == false)
            SetDifficulty();
        else
        {
            weaponChoiceUI.SetActive(false);
            ExitStartWeaponMenu();
        }
    }
    public void SetDifficulty()
    {
        weaponChoiceUI.SetActive(false);
        difficultySet.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(difficultySet.gameObject.GetComponentInChildren<Slider>().gameObject);
    }
}
