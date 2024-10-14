using System.Collections.Generic;
using UnityEngine;

public class BSPTester : MonoBehaviour {

    [SerializeField] private GameObject m_pointPrefab;
    [SerializeField] private GameObject m_medianPrefab;
    [SerializeField] private int        m_numberOfPoints;

    [Header("Debug")] [SerializeField] private int m_depth;

    private PointCloud       m_pointCloud;
    private BSPTree          m_bspTree;
    private List<GameObject> m_points = new();

    private void Start() {
        m_pointCloud = new PointCloud(m_numberOfPoints);
        CreateBSPTree();

        SpawnPoints();
    }

    private void Update() {
        m_pointCloud.RegeneratePoints();
        CreateBSPTree();
    }

    private void CreateBSPTree() {
        m_bspTree = new BSPTree(m_pointCloud.m_points);
    }

    private void SpawnPoints() {
        foreach (var point in m_pointCloud.m_points) {
            //GameObject pointObject = Instantiate(m_pointPrefab, point.m_position, Quaternion.identity);
            //m_points.Add(pointObject);
        }
    }

    private void OnDrawGizmos() {
        if (m_bspTree != null) {
            foreach (var node in m_bspTree.m_tree) {
                if (node.m_depth <= m_depth) {
                    Gizmos.color = new Color(0.67f / (node.m_depth + 1), 0.22f / (node.m_depth + 1), 0.81f / (node.m_depth + 1), 1.0f);
                    Gizmos.DrawSphere(node.m_point.m_position, 0.2f);
                }
                else {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawSphere(node.m_point.m_position, 0.2f);
            }
        }
    }
}