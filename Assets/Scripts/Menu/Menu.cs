using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public TextMeshProUGUI highScore;
    public GameObject firstChoice;
    private void Start()
    {
        menu.gameObject.SetActive(true);
        highScore.text = "High Score:\n" + PlayerPrefs.GetInt("highScore").ToString();

        EventSystem.current.SetSelectedGameObject(firstChoice);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
