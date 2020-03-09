using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject m_LaserPrefab;

    [SerializeField]
    float m_MoveSpeed = 10.0f;

    [SerializeField]
    float m_Padding = 1.0f;

    [SerializeField]
    float m_ProjectileSpeed = 10.0f;

    [SerializeField]
    float m_FiringPeriod = 0.05f;

    // Local variables
    float m_xMin = 0.0f;
    float m_yMin = 0.0f;
    float m_xMax = 0.0f;
    float m_yMax = 0.0f;

    Coroutine m_FiringCoroutine;

    private void Start()
    {
        SetupMoveBoundaries();
        if (m_LaserPrefab == null)
        {
            Debug.LogError("LaserPrefab is null!");
        }
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float deltaX = m_MoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float deltaY = m_MoveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        float newXPos = Mathf.Clamp(transform.position.x + deltaX, m_xMin, m_xMax);
        float newYPos = Mathf.Clamp(transform.position.y + deltaY, m_yMin, m_yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_FiringCoroutine = StartCoroutine(FireContinuously());
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(m_FiringCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(
                m_LaserPrefab,
                transform.position,
                Quaternion.identity) as GameObject; // Quaternion.identity -> da nema nikakve rotacije
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_ProjectileSpeed);
            yield return new WaitForSeconds(m_FiringPeriod);
        }
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;

        m_xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + m_Padding;
        m_xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - m_Padding;

        m_yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + m_Padding;
        m_yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - m_Padding;
    }
}
