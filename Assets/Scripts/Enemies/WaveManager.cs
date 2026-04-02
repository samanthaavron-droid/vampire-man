using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject[] prefab;

    public GameObject[] spawnPos;

    public GameObject[] countDown;

    public int waveCount; //how many enemies in the starting wa
    public int waveIterationModulator; //how many waves until upgrade

    public Slider slider;

    private int waveIteration;
    private int waveUpgrade;

    public GameObject player;

    public event Action<WaveManager> onSpawn;

    [HideInInspector]public List<EnemyAI> activeEnemies = new List<EnemyAI>();
    void Start()
    {
        waveIteration = 0;
        waveUpgrade = 0;

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
    public void SetWaveCount()
    {
        waveCount = Mathf.RoundToInt(slider.value);
        if (waveCount > 10)
        {

        }
    }
    public void Spawner()
    {
        waveIteration += 1; //keeping track what wave is it

        if ((waveIteration % waveIterationModulator) == 0)
        {
            waveUpgrade += 1;
        }

        for (int i = 0; i < waveCount; i++)
        {
            Vector3 spawnPosition = CheckPosition(spawnPos[GenerateUniqueRandoms(1, 0, spawnPos.Length)[0]].transform.position);

            GameObject newEnemy = Instantiate(prefab[UnityEngine.Random.Range(0, prefab.Length)], spawnPosition, Quaternion.identity);

            activeEnemies.Add(newEnemy.GetComponent<EnemyAI>());
            newEnemy.GetComponent<EnemyAI>().OnDeath += HandleDeath;

            if (waveUpgrade > 0)
            {
                UpgradeEnemies(newEnemy);
            }
        }
        onSpawn?.Invoke(this);
        waveCount += Mathf.RoundToInt(waveCount / 5); // increasing wave size
    }
    private Vector3 CheckPosition(Vector3 position)
    {
        while (Vector3.Distance(position, player.transform.position) > 20f)
        {
            position = spawnPos[GenerateUniqueRandoms(1, 0, spawnPos.Length)[0]].transform.position;
        }
        return position;
    }
    private void HandleDeath(EnemyAI dead)
    {
        dead.OnDeath -= HandleDeath;
        activeEnemies.Remove(dead);

        if (activeEnemies.Count == 0)
        {
            StartCoroutine(NewWave());
        }
    }
    private void UpgradeEnemies(GameObject newEnemy)
    {
        for (int i = 0; i < waveUpgrade; i++)
        {
            UniversalBody enemyBody = newEnemy.GetComponent<UniversalBody>();

            enemyBody.MainDamageUpgrade();
            enemyBody.MainSpeedUpgrade();
            enemyBody.MainSizeUpgrade();
            enemyBody.MainRechargeUpgrade();
            enemyBody.HealthUpgrade();
            enemyBody.SpeedUpgrade();
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
