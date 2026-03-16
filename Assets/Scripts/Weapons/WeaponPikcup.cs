using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponPikcup : MonoBehaviour
{
    [HideInInspector]public Weapons playerChoice;

    private ChestSpawner chestSpawner;
    private void Start()
    {
        chestSpawner = GetComponentInParent<ChestSpawner>();        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerChoice = collision.GetComponent<Weapons>();
            chestSpawner.weaponChoiceUI.SetActive(true);
            collision.GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
            chestSpawner.ChestOpened();
        }
    }
}