using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyFinder : MonoBehaviour
{
    public GameObject waveManager;
    public GameObject prefab;
    public GameObject prefabParent;
    public GameObject player;

    private List<EnemyTracker> trackers = new List<EnemyTracker>();
    class EnemyTracker
    {
        public EnemyAI trackTarget;
        public GameObject trackerParent;
        public GameObject trackerDisplay;
    }
    void Start()
    {
        if (waveManager.GetComponent<WaveManager>() != null)
        {
            waveManager.GetComponent<WaveManager>().onSpawn += EnemyTrackerUpdate;
        } else
        {
            waveManager.GetComponent<TutorialWaves>().onSpawn += EnemyTrackerUpdateTutorial;
        }
    }

    void Update()
    {
        TrackEnemies();
    }
    private void EnemyTrackerUpdate(WaveManager wave)
    {
        EnemyTrackerUpdate(wave.activeEnemies);
    }
    private void EnemyTrackerUpdateTutorial(TutorialWaves wave)
    {
        EnemyTrackerUpdate(wave.activeEnemies);
    }
    private void EnemyTrackerUpdate(List<EnemyAI> wave)
    {
        foreach (EnemyAI enemy in wave)
        {
            Vector3 spawnPositionP = player.transform.position;
            GameObject newTrackerParent = Instantiate(prefabParent, spawnPositionP, Quaternion.identity);

            Vector3 spawnPositionTr = new Vector3(newTrackerParent.transform.position.x + 1f, newTrackerParent.transform.position.y, newTrackerParent.transform.position.z);
            GameObject newTracker = Instantiate(prefab, spawnPositionTr, Quaternion.identity);

            newTracker.transform.SetParent(newTrackerParent.transform);

            trackers.Add(new EnemyTracker()
            { 
            trackerDisplay = newTracker,
            trackerParent = newTrackerParent,
            trackTarget = enemy
            });
        }
    }
    private void TrackEnemies()
    {
        for (int i = trackers.Count - 1; i >= 0; i--)
        {
            EnemyTracker tracker = trackers[i];
            if (tracker.trackTarget.IsDestroyed() == false)
            {
                Vector2 direction = trackers[i].trackTarget.transform.position - player.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                tracker.trackerParent.transform.rotation = Quaternion.Euler(0, 0, angle);

                tracker.trackerParent.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

            }
            else
            {
                trackers.RemoveAt(i);
                Destroy(tracker.trackerDisplay);
                Destroy(tracker.trackerParent);
            }
        }
    }
}