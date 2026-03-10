using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathUI;
    public GameObject restart;
    public PlayerInput playerInp;

    public string thisScene;
    void Start()
    {
        deathUI.GetComponentInChildren<TextMeshProUGUI>().text = "High Score:\n" + PlayerPrefs.GetInt("highScore").ToString();
        deathUI.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PlayerBase.dead == true)
        {
            deathUI.gameObject.SetActive(true);
            playerInp.SwitchCurrentActionMap("UI");
            EventSystem.current.SetSelectedGameObject(restart);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(thisScene);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
