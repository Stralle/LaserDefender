using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField]
    GameObject m_EnemyPrefab;

    [SerializeField]
    GameObject m_PathPrefab;

    [SerializeField]
    float m_TimeBetweenSpawns = 0.5f;

    [SerializeField]
    float m_SpawnRandomFactor = 0.3f;

    [SerializeField]
    int m_NumberOfEnemies = 5;

    [SerializeField]
    float m_MoveSpeed = 2.0f;

    public GameObject GetEnemyPrefab() { return m_EnemyPrefab; }
    public List<Transform> GetWaypoints()
    {
        List<Transform> waveWaypoints = new List<Transform>();

        foreach (Transform child in m_PathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints; 
    }
    public float GetTimeBetweenSpawns() { return m_TimeBetweenSpawns; }
    public float GetSpawnRandomFactor() { return m_SpawnRandomFactor; }
    public int GetNumberOfEnemies() { return m_NumberOfEnemies; }
    public float GetMoveSpeed() { return m_MoveSpeed; }


}
