using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;

public class WaveManager : MonoBehaviour
{
    public GameObject[] prefab;

    public GameObject[] spawnPos;

    public GameObject[] countDown;

    public int waveCount;

    public event Action<WaveManager> onSpawn;

    [HideInInspector]public List<EnemyAI> activeEnemies = new List<EnemyAI>();
    void Start()
    {
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
    public void Spawner()
    {
        for (int i = 0; i < waveCount; i++)
        {
            Vector3 spawnPosition = spawnPos[GenerateUniqueRandoms(1, 0, spawnPos.Length)[0]].transform.position;

            GameObject newEnemy = Instantiate(prefab[UnityEngine.Random.Range(0, prefab.Length)], spawnPosition, Quaternion.identity);

            activeEnemies.Add(newEnemy.GetComponent<EnemyAI>());
            newEnemy.GetComponent<EnemyAI>().OnDeath += HandleDeath;
        }
        onSpawn?.Invoke(this);
    }
    private void HandleDeath(EnemyAI dead)
    {
        dead.OnDeath -= HandleDeath;
        activeEnemies.Remove(dead);
        onSpawn?.Invoke(this);

        if (activeEnemies.Count == 0)
        {
            StartCoroutine(NewWave());
        }
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
    private IEnumerator NewWave()
    {
        countDown[4].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[4].gameObject.SetActive(false);
        countDown[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[3].gameObject.SetActive(false);
        countDown[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[2].gameObject.SetActive(false);
        countDown[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[1].gameObject.SetActive(false);
        countDown[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        countDown[0].gameObject.SetActive(false);

        Spawner();
    }
}
