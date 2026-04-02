using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChestSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] spawnPos;

    public GameObject weaponChoiceUI;
    public Button[] buttons;

    private WeaponChoice[] weaponChoices = new WeaponChoice[2];
    private WeaponPikcup chest;
    void Start()
    {
        if (spawnPos == null)
        {
            spawnPos = new GameObject[24];
            spawnPos = GameObject.FindGameObjectsWithTag("chestSpawn");
        }

        weaponChoiceUI.SetActive(false);
        Spawner();
    }
    private void Spawner()
    {
        Vector3 spawnPosition = spawnPos[Random.Range(0, spawnPos.Length)].transform.position;
        GameObject newChest = Instantiate(prefab, spawnPosition, Quaternion.identity);
        newChest.transform.SetParent(transform);
        chest = newChest.GetComponent<WeaponPikcup>();
    }
    public void ChestOpened()
    {
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(buttons[2].gameObject);
        SetWeapon();
    }
    private void SetWeapon()
    {
        weaponChoices[0] = RandomWeapon();
        weaponChoices[1] = RandomWeapon();

        if (weaponChoices[0] == chest.playerChoice.mWeapon || weaponChoices[1] == chest.playerChoice.mWeapon || weaponChoices[0] == weaponChoices[1])
        {
            RandomWeapon();
            Debug.Log("weapon reroll");

            SetWeapon();
            return;
        }
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = weaponChoices[0].ToString();
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = weaponChoices[1].ToString();
    }
    private WeaponChoice RandomWeapon()
    {
        System.Array values = System.Enum.GetValues(typeof(WeaponChoice));
        WeaponChoice weapon = (WeaponChoice)values.GetValue(UnityEngine.Random.Range(0, 5));
        return weapon;
    }
    public void WeaponChoiceButton(int i)
    {
        chest.playerChoice.sWeapon = weaponChoices[i];
        chest.playerChoice.SetWeapon();
        weaponChoiceUI.SetActive(false);
        chest.playerChoice.gameObject.GetComponent<PlayerBase>().SetWeaponRechargeUI();
        chest.playerChoice.gameObject.GetComponent<PlayerInput>().SwitchCurrentActionMap("playerController");
        Time.timeScale = 1f;
        Destroy(chest.gameObject);
    }
}
