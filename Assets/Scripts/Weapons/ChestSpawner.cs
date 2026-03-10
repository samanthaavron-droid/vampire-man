using UnityEngine;

public class ChestSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] spawnPos;
    public GameObject weaponChoiceUI;
    void Start()
    {
        if (spawnPos == null)
        {
            spawnPos = GameObject.FindGameObjectsWithTag("chestSpawn");
        }
        
        weaponChoiceUI.SetActive(false);
        Spawner();
    }
    private void Spawner()
    {
        Vector3 spawnPosition = spawnPos[Random.Range(0, spawnPos.Length)].transform.position;
        GameObject newChest = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
