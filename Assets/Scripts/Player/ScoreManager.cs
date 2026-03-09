using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int levelXP;
    [SerializeField] private int xpToLevel;

    public GameObject weaponChoiceUI;
    public GameObject firstButtonWeapon;
    public GameObject player;

    public PlayerInput playerInput;

    public bool resetScore;
    public bool testUpgrade;

    private string[] upgrades = {"MainDamageUpgrade", "MainSpeedUpgrade", "MainSizeUpgrade", "MainRechargeUpgrade",
                                "SecondaryDamageUpgrade", "SecondarySpeedUpgrade", "SecondarySizeUpgrade", "SecondaryRechargeUpgrade", "HealthUpgrade", "SpeedUpgrade"};
    private int[] chosenUpgrades;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        chosenUpgrades = new int[3];

        weaponChoiceUI.SetActive(false);
        firstButtonWeapon.SetActive(false);
        
        if (testUpgrade)
            Upgrade(); //to see upgrade menu from the start

        if (resetScore)
            AnnulateHighScore(); //to set high score to 0
    }
    private void Update()
    {
        if (levelXP >= xpToLevel)
        {
            Upgrade();
        }
    }
    public void AddXP(int xp) //public Method that is called when entity dies
    {
        if (PlayerPrefs.GetInt("highScore", 0) > levelXP)
        {
            PlayerPrefs.SetInt("highScore", levelXP);
            PlayerPrefs.Save();
        } //high score save

        levelXP += xp;

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
    private void AnnulateHighScore()
    {
        PlayerPrefs.SetInt("highScore", 0);
        PlayerPrefs.Save();
    }
    private void GetRandomUpgrades()
    {
        chosenUpgrades = GenerateUniqueRandoms(3, 0, upgrades.Length);
        
        if (chosenUpgrades.Contains(4) || chosenUpgrades.Contains(5) || chosenUpgrades.Contains(6) || chosenUpgrades.Contains(7))
        {
            GetRandomUpgrades();
            return;
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
