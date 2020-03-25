using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    WaveConfig m_WaveConfig;

    List<Transform> m_Waypoints = new List<Transform>();
    int m_WaypointIndex = 0;
    float m_MoveSpeed = 0.0f;

    void Start()
    {
        if (m_WaveConfig != null)
        {
            m_Waypoints = m_WaveConfig.GetWaypoints();
            m_MoveSpeed = m_WaveConfig.GetMoveSpeed();
            transform.position = m_Waypoints[m_WaypointIndex].transform.position;
        }
        Debug.Log("Waypoints count: " + m_Waypoints.Count);
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        m_WaveConfig = waveConfig;
    }

    private void Move()
    {
        if (m_WaypointIndex < m_Waypoints.Count)
        {
            var targetPosition = m_Waypoints[m_WaypointIndex].transform.position;
            var movementThisFrame = m_MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                m_WaypointIndex++;
                Debug.Log(m_WaypointIndex);
            }
        }
        else
        {
            Destroy(gameObject); 
            Debug.Log("Destroy " + m_WaypointIndex);
        }
    }
}
