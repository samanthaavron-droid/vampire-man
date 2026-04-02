using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Explanation : MonoBehaviour
{
    public Image[] images; //(0) blackscreen
    public GameObject button;
    public PlayerInput playerInput;
    public PlayerBase playerBase;

    private int slide;
    void Start()
    {
        slide = 1;

        foreach (var image in images)
        {
            image.CrossFadeAlpha(0f, 0f, true);
            image.gameObject.SetActive(true);

            if (image.GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                image.GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(0f, 0f, true);
            }
        }

        images[0].CrossFadeAlpha(1f, 0f, true);
        button.gameObject.SetActive(true);
        StartTutorial();
    }
    public void StartTutorial()
    {
        Time.timeScale = 0f;

        images[1].CrossFadeAlpha(0f, 0f, true);
        images[1].CrossFadeAlpha(1f, 2f, true);

        if (images[1].GetComponentInChildren<TextMeshProUGUI>() != null)
            images[1].GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(1f, 2f, true);



        EventSystem.current.SetSelectedGameObject(button);
        playerInput.SwitchCurrentActionMap("UI"); //switching input
    }
    public void NextSlide()
    {
        if (slide < images.Length - 1)
        {
            images[slide].CrossFadeAlpha(0f, 0.5f, true);

            if (images[slide].GetComponentInChildren<TextMeshProUGUI>() != null)
            {
                images[slide].GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(0f, 0.2f, true);
            }

            images[slide + 1].CrossFadeAlpha(1f, 2f, true);

            if (images[slide + 1].GetComponentInChildren<TextMeshProUGUI>()  != null)
            {
                images[slide + 1].GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(1f, 2f, true);
            }

            slide++;
        } else
        {
            images[slide].CrossFadeAlpha(0f, 1f, true);

            if (images[slide].GetComponentInChildren<TextMeshProUGUI>() != null)
                images[slide].GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(0f, 0.5f, true);


            images[0].CrossFadeAlpha(0f, 1f, true);
            button.GetComponentInChildren<TextMeshProUGUI>().CrossFadeAlpha(0f, 0.5f, true);
            button.GetComponentInChildren<Image>().CrossFadeAlpha(0f, 0.5f, true);

            playerBase.chooseWeapon = true;
        }
    }
}
