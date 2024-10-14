using UnityEngine;

public class BSPNode {

    public Point m_point { get; private set; }
    public int   m_depth { get; private set; }

    public BSPNode m_leftChild;
    public BSPNode m_rightChild;
    
    public Vector3 m_plane;

    public BSPNode(Point _point, int _depth) {
        m_point = _point;
        m_depth = _depth;
    }
}