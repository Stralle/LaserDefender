using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField]
    float m_MoveSpeed = 2.0f;

    [SerializeField]
    WaveConfig m_WaveConfig;

    List<Transform> m_Waypoints = new List<Transform>();
    int m_WaypointIndex = 0;

    void Start()
    {
        m_Waypoints = m_WaveConfig.GetWaypoints();
        transform.position = m_Waypoints[m_WaypointIndex].transform.position;
        Debug.Log("Waypoints count: " + m_Waypoints.Count);
    }

    void Update()
    {
        Move();
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
