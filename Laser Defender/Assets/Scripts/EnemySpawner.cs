using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    List<WaveConfig> m_WaveConfigs;

    [SerializeField]
    int m_StartingWave = 0;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = m_StartingWave; waveIndex < m_WaveConfigs.Count; waveIndex++)
        {
            var currentWave = m_WaveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount < currentWave.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }
}
