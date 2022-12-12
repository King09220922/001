using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticleMove : MonoBehaviour
{
    [Header("运行后销毁路线对象")] public bool m_clearWhenStart;
    public Transform m_allRoutePoints;

    public List<Vector3> m_routePoints;
    public int m_currentPointIndex;

    [Header("粒子移动速度")] public float m_moveSpeed;

    private void Start()
    {
        m_routePoints = new List<Vector3>();
        for (int i = 0; i < m_allRoutePoints.childCount; i++)
        {
            m_routePoints.Add(m_allRoutePoints.GetChild(i).position);
        }
        transform.position = m_routePoints[0];

        if (m_clearWhenStart)
        {
            for (int i = m_allRoutePoints.childCount - 1; i >= 0; i--)
            {
                Destroy(m_allRoutePoints.gameObject);
            }
        }
    }


    private void FixedUpdate()
    {
        transform.LookAt(m_routePoints[m_currentPointIndex]);
        transform.position += transform.forward * m_moveSpeed * Time.deltaTime; // = Vector3.LerpUnclamped(transform.position, m_routePoints[m_currentPointIndex], m_moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, m_routePoints[m_currentPointIndex]) <= 0.2f)
        {
            m_currentPointIndex = (m_currentPointIndex + 1) % m_routePoints.Count;
        }
    }
}
