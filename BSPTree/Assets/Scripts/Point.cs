using UnityEngine;

public class Point {
    
    public Vector3 m_position { get; private set; }
    
    public Point(Vector3 _position) {
        m_position = _position;
    }
}
