using UnityEngine;
using System.Collections.Generic;

public class Seeker : MonoBehaviour
{
    public Transform[] target;

    internal List<Node> path;
    internal int targetIndex;

    [HideInInspector]public bool ignorePoints;
    private void Awake()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("PathDot");

        if (points.Length > 0)
        {
            FindPathDot(points);
        }
    }
    private void Start()
    {
        // Оновлюємо шлях 4 рази на секунду
        RequestPath();
    }

    public void RequestPath()
    {
        if (target != null && Pathfinding.instance != null)
        {
           // Debug.Log("RequestPath " );
            path = Pathfinding.instance.FindPath(transform.position, target[0].position);

            if (path != null && path.Count > 0 && ignorePoints == false)
            {
                targetIndex = 0;
            } 
            else
            {
                path = Pathfinding.instance.FindPath(transform.position, target[1].position);

                if (path != null && path.Count > 0)
                {
                    targetIndex = 0;
                }
            }
        }
    }
    private void FindPathDot(GameObject[] points)
    {
        int randomIndex = Random.Range(0, points.Length);
        target = new Transform[2];

        target[0] = points[randomIndex].transform;
        target[1] = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}