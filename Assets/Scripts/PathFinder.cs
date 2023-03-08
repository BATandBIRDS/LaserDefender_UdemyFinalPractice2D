using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    EnemySpawner spawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int wayPointIndex = 0;

    void Awake()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = spawner.GetCurrentWave();
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[wayPointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if(wayPointIndex < waypoints.Count)
        {
            Vector3 targetPos = waypoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, delta);
            if (transform.position == targetPos)
            {
                wayPointIndex++;
            }
        }
        else
        {
            // DESPAWN
            Destroy(gameObject);
        }
    }
}
