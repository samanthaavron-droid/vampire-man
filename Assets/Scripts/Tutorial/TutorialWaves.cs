using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialWaves : MonoBehaviour
{
    public waves[] waves;
    public GameObject[] spawnPos;
    public GameObject[] countDown;

    public List<EnemyAI> activeEnemies = new List<EnemyAI>();
    private int waveN;

    public GameObject finishUI;
    public GameObject emptyButton;
    public PlayerInput playerInp;

    public event Action<TutorialWaves> onSpawn;
    void Start()
    {
        finishUI.SetActive(false);

        waveN = -1;
        if (spawnPos == null)
        {
            spawnPos = GameObject.FindGameObjectsWithTag("spawnPos");
        }
        foreach (var count in countDown)
        {
            count.gameObject.SetActive(false);
        }
        StartCoroutine(NewWave());
    }
    private void Spawner()
    {
        if (waveN < waves.Length)
        {
            for (int i = 0; i < waves[waveN].wave.Length; i++)
            {
                Vector3 spawnPosition = spawnPos[GenerateUniqueRandoms(1, 0, spawnPos.Length)[0]].transform.position;
                GameObject newEnemy = Instantiate(waves[waveN].wave[UnityEngine.Random.Range(0, waves[waveN].wave.Length)], spawnPosition, Quaternion.identity);

                activeEnemies.Add(newEnemy.GetComponent<EnemyAI>());
                newEnemy.GetComponent<EnemyAI>().OnDeath += HandleDeath;
            }
        } else
        {
            NoWaves();
        }
        onSpawn?.Invoke(this);
    }
    private void HandleDeath(EnemyAI dead)
    {
        dead.OnDeath -= HandleDeath;
        activeEnemies.Remove(dead);
        //onSpawn?.Invoke(this);

        if (activeEnemies.Count == 0)
        {
            StartCoroutine(NewWave());
        }
    }
    private void NoWaves()
    {
        Time.timeScale = 0f;
        finishUI.SetActive(true);
        EventSystem.current.SetSelectedGameObject(emptyButton);
        playerInp.SwitchCurrentActionMap("UI");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator NewWave()
    {
        countDown[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[2].gameObject.SetActive(false);
        countDown[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[1].gameObject.SetActive(false);
        countDown[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[0].gameObject.SetActive(false);

        waveN += 1;
        Spawner();
    }
    int[] GenerateUniqueRandoms(int amount, int min, int max)
    {
        List<int> uniqueNumbers = new List<int>();

        while (uniqueNumbers.Count < amount)
        {
            int randomVal = UnityEngine.Random.Range(min, max);
            if (!uniqueNumbers.Contains(randomVal))
            {
                uniqueNumbers.Add(randomVal);
            }
        }
        return uniqueNumbers.ToArray();
    }
}

[System.Serializable]
public struct waves
{
    public GameObject[] wave;
}