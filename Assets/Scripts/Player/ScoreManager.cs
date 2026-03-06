using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField] private int levelXP;
    [SerializeField] private int xpToLevel;

    public GameObject weaponChoiceUI;
    public GameObject firstButtonWeapon;

    public PlayerInput playerInput;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        weaponChoiceUI.SetActive(false);
        firstButtonWeapon.SetActive(false);

        //Upgrade(); //to see upgrade menu from the start

        //AnnulateHighScore(); //to set high score to 0
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
}
