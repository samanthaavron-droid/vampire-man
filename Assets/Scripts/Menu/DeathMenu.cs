using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public GameObject highScore;
    public string thisScene;
    void Start()
    {
        //highScore.text = "High Score: " + PlayerPrefs.GetInt("highScore").ToString();
        highScore.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (PlayerBase.dead == true)
        {
            highScore.gameObject.SetActive(true);
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
