using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponPikcup : MonoBehaviour
{
    private WeaponChoice weapon1;
    private WeaponChoice weapon2;
    private Weapons playerChoice;

    public GameObject weaponChoiceUI;
    public GameObject emptyButton;
    public Button choice1;
    public Button choice2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Weapons player = collision.GetComponent<Weapons>();
            weaponChoiceUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(emptyButton);
            collision.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
            Time.timeScale = 0f;
            SetWeapon(player);
            playerChoice = player;
        }
    }
    private WeaponChoice RandomWeapon()
    {
        System.Array values = System.Enum.GetValues(typeof(WeaponChoice));
        WeaponChoice weapon = (WeaponChoice)values.GetValue(UnityEngine.Random.Range(0, 5));
        return weapon;
    }
    private void SetWeapon(Weapons player)
    {
        weapon1 = RandomWeapon();
        weapon2 = RandomWeapon();

        if (weapon1 == player.mWeapon || weapon2 == player.mWeapon || weapon1 == weapon2)
        {
            RandomWeapon();
            Debug.Log("weapon reroll");

            SetWeapon(player);
            return;
        }
        choice1.GetComponentInChildren<TextMeshProUGUI>().text = weapon1.ToString();
        choice2.GetComponentInChildren<TextMeshProUGUI>().text = weapon2.ToString();
    }
    public void ChoiceOne()
    {
        playerChoice.sWeapon = weapon1;
        playerChoice.SetWeapon();
        weaponChoiceUI.SetActive(false);
        playerChoice.gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("playerController");
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
    public void ChoiceTwo()
    {
        playerChoice.sWeapon = weapon2;
        playerChoice.SetWeapon();
        weaponChoiceUI.SetActive(false);
        playerChoice.gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("playerController");
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}