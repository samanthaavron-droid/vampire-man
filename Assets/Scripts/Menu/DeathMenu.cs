using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DeathMenu : MonoBehaviour
{
    public GameObject deathUI;

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
