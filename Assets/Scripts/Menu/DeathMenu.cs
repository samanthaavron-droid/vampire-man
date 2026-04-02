using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathUI;
    public Button empty;
    public PlayerInput playerInp;
    public TMP_InputField inputField;
    public TextMeshProUGUI yourScore;
    public GameObject accept;
    public GameObject upgradeUI;

    public string thisScene;
    public leaderboard board;

    public VirtualKeyboard keyboard;
    public GameObject positon;

    private int _rankAchinved = -1;
    void Start()
    {
        deathUI.gameObject.SetActive(false);

        if (keyboard != null)
            keyboard.gameObject.SetActive(false);


        if (inputField != null)
            inputField.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PlayerBase.dead == true && !deathUI.gameObject.activeSelf)
        {
            int currentScore = PlayerPrefs.GetInt("currentScore", 0);
            upgradeUI.gameObject.SetActive(false);

            if (thisScene != "Tutorial")
            {
                for (int i = 1; i <= 12; i++)
                {
                    int scoreComp = PlayerPrefs.GetInt(i.ToString() + "score", 0);

                    if (currentScore > scoreComp)
                    {
                        _rankAchinved = i; break;
                    }
                }
                if (_rankAchinved != -1)
                {
                    StartCoroutine(KeyboardTrigger());
                }
                else
                {
                    accept.gameObject.SetActive(false);
                    inputField.gameObject.SetActive(false);
                    EventSystem.current.SetSelectedGameObject(empty.gameObject);
                }

            }
            else
            {
                EventSystem.current.SetSelectedGameObject(empty.gameObject);

                if (yourScore != null)
                    yourScore.gameObject.transform.position = positon.transform.position;
            }

            if (yourScore != null)
                yourScore.text += "\n" + currentScore.ToString();

            deathUI.gameObject.SetActive(true);
            playerInp.SwitchCurrentActionMap("UI");
            playerInp.gameObject.GetComponent<UniversalBody>().stats.movementSpeed = 0f;
        }
    }
    private IEnumerator KeyboardTrigger()
    {
        yield return new WaitForSecondsRealtime(2f);

        inputField.gameObject.SetActive(true);
        keyboard.gameObject.SetActive(true);
        keyboard.targetInpitField = inputField;
        EventSystem.current.SetSelectedGameObject(empty.gameObject);
        Time.timeScale = 0;
    }
    public void RecordName()
    {
        if (inputField.text == null)
        {
            inputField.gameObject.SetActive(false);
            accept.SetActive(false);
            return;
        }

        for (int i = 12; i > _rankAchinved; i--)
        {
            string prevName = PlayerPrefs.GetString((i - 1) + "name", "AAA");
            int prevScore = PlayerPrefs.GetInt((i - 1) + "score", 0);

            PlayerPrefs.SetString(i + "name", prevName);
            PlayerPrefs.SetInt(i + "score", prevScore);
        }

        string playerName = inputField.text;

        PlayerPrefs.SetString(_rankAchinved + "name", playerName);
        PlayerPrefs.SetInt(_rankAchinved + "score", PlayerPrefs.GetInt("currentScore", 0));
        PlayerPrefs.Save();

        board.UpdateLeaderboardDisplay();
        inputField.gameObject.SetActive(false);
        accept.SetActive(false);
        EventSystem.current.SetSelectedGameObject(empty.gameObject);
        yourScore.gameObject.transform.position = positon.transform.position;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(thisScene);
    }
    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
