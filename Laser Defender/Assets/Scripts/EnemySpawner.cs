using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> m_WaveConfigs;

    int m_StartingWave = 0;

    void Start()
    {
        var currentWave = m_WaveConfigs[m_StartingWave];
        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount < currentWave.GetNumberOfEnemies(); enemyCount++)
        {
            Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }
}
