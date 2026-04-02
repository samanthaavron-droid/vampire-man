using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] public static int levelXP;
    [SerializeField] private int xpToLevel;

    public GameObject weaponChoiceUI;
    public GameObject firstButtonWeapon;
    public GameObject player;

    public TextMeshProUGUI scoreDisplay;

    public Button[] buttons;

    public PlayerInput playerInput;

    private string[] upgrades = {"MainDamageUpgrade", "MainSpeedUpgrade", "MainSizeUpgrade", "MainRechargeUpgrade",
                                "SecondaryDamageUpgrade", "SecondarySpeedUpgrade", "SecondarySizeUpgrade", "SecondaryRechargeUpgrade",
                                "HealthUpgrade", "SpeedUpgrade"};
    private string[] upgradeNames = {"Damage Upgrade", "Attack Speed Upgrade", "Size Upgrade", "Cooldown Upgrade",
                                    "Damage Upgrade", "Attack Speed Upgrade", "Size Upgrade", "Cooldown Upgrade",
                                    "Health Upgrade", "Speed Upgrade"};
    private int[] chosenUpgrades;
    private void Awake()
    {
        Instance = this;
        levelXP = 0;
    }
    private void Start()
    {
        chosenUpgrades = new int[3];

        weaponChoiceUI.SetActive(false);

        PlayerPrefs.SetInt("currentScore", 0);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if (levelXP >= xpToLevel)
        {
            Upgrade();
        }
        scoreDisplay.text = "Score: " + levelXP.ToString();
    }
    public void AddXP(int xp) //public Method that is called when entity dies
    {
        levelXP += xp;

        PlayerPrefs.SetInt("currentScore", levelXP);
        PlayerPrefs.Save();//high score save

        Debug.Log("Current XP: " + levelXP + ", XP needed: " + xpToLevel);
    }
    private void Upgrade()
    {
        xpToLevel += xpToLevel + Mathf.RoundToInt(xpToLevel / 10); //increasing xp cost

        Time.timeScale = 0; //stopping time so u can choose an upgrade
        weaponChoiceUI.SetActive(true); //activating upgrade interface
        GetRandomUpgrades();

        playerInput.SwitchCurrentActionMap("UI"); //switching input

        EventSystem.current.SetSelectedGameObject(firstButtonWeapon); //selecting the default button
    }
    public static void CloseMenu()
    {
        Instance.playerInput.SwitchCurrentActionMap("playerController");
        Instance.weaponChoiceUI.SetActive(false);
        Time.timeScale = 1;
    }
    private void GetRandomUpgrades()
    {
        chosenUpgrades = GenerateUniqueRandoms(3, 0, upgrades.Length);

        if (player.GetComponent<Weapons>().sWeapon == WeaponChoice.None)
        {
            if (chosenUpgrades.Contains(4) || chosenUpgrades.Contains(5) || chosenUpgrades.Contains(6) || chosenUpgrades.Contains(7))
            {
                GetRandomUpgrades();
                return;
            }
        }
        SetButtonNames();
    }
    private void SetButtonNames()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (chosenUpgrades[i] == 0 || chosenUpgrades[i] == 1 || chosenUpgrades[i] == 2 || chosenUpgrades[i] == 3)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = player.GetComponent<Weapons>().mWeapon.ToString()
                                                                            + "\n" + upgradeNames[chosenUpgrades[i]].ToString();
            } else if (chosenUpgrades[i] == 4 || chosenUpgrades[i] == 5 || chosenUpgrades[i] == 6 || chosenUpgrades[i] == 7)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = player.GetComponent<Weapons>().sWeapon.ToString()
                                                                            + "\n" + upgradeNames[chosenUpgrades[i]].ToString();
            } else if (chosenUpgrades[i] == 8 || chosenUpgrades[i] == 9)
            {
                buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = upgradeNames[chosenUpgrades[i]].ToString();
            }
        }
    }
    public void FirstUpgradeButton()
    {
        string chosen = upgrades[chosenUpgrades[0]];
        player.GetComponent<UniversalBody>().Invoke(chosen, 0f);
    }
    public void SecondUpgradeButton()
    {
        string chosen = upgrades[chosenUpgrades[1]];
        player.GetComponent<UniversalBody>().Invoke(chosen, 0f);
    }
    public void ThirdUpgradeButton()
    {
        string chosen = upgrades[chosenUpgrades[2]];
        player.GetComponent<UniversalBody>().Invoke(chosen, 0f);
    }
    int[] GenerateUniqueRandoms(int amount, int min, int max)
    {
        List<int> uniqueNumbers = new List<int>();

        while (uniqueNumbers.Count < amount)
        {
            int randomVal = Random.Range(min, max);
            if (!uniqueNumbers.Contains(randomVal))
            {
                uniqueNumbers.Add(randomVal);
            }
        }
        return uniqueNumbers.ToArray();
    }
}
