using System.Collections.Generic;
using UnityEngine;

public class PointCloud {

    private float m_rotationSpeed;

    private readonly Vector3 m_limits = new(10, 10, 10);

    public readonly List<Point> m_points = new();

    public PointCloud(int _numberOfPoints) {
        GeneratePoints(_numberOfPoints);
    }

    private void GeneratePoints(int _numberOfPoints) {
        for (int i = 0; i < _numberOfPoints; i++) {
            Vector3 position = new(
                Random.Range(-m_limits.x, m_limits.x),
                Random.Range(-m_limits.y, m_limits.y),
                Random.Range(-m_limits.z, m_limits.z)
            );

            Point point = new(position);
            m_points.Add(point);
        }
    }
    
    public void RegeneratePoints() {
        int numberOfPoints = m_points.Count;
        m_points.Clear();
        
        GeneratePoints(numberOfPoints);
    }
}