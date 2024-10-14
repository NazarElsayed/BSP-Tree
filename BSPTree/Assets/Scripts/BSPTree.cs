using System.Collections.Generic;
using UnityEngine;

public class BSPTree {

    private const int c_XAxis = 0;

    public List<BSPNode> m_tree { get; private set; }
    
    public BSPTree(List<Point> _points) {
        CreateTree(_points);
    }

    private void CreateTree(List<Point> _points) {
        
        List<Point> childNodes = new();
        childNodes.AddRange(_points);

        Point median = FindMedianPoint(childNodes, _points.Count / 2, 0, c_XAxis);
        childNodes.Remove(median);

        m_tree = new List<BSPNode>();
        
        BSPNode node = new(median, 0);
        CreateChildNodes(node, _points, c_XAxis);
    }

    private static Point FindMedianPoint(List<Point> _candidates, int _midPoint, int _startIndex, int _axis) {
        
        Point result;
        
        //QuickSelect Algorithm
        List<Point> lesserElements  = new();
        List<Point> greaterElements = new();

        Point pivot      = _candidates[Random.Range(0, _candidates.Count)];
        float pivotValue = pivot.m_position[_axis];

        foreach (Point point in _candidates) {
            if (point == pivot) {
                continue;
            }

            float pointValue = point.m_position[_axis];

            if (pointValue <= pivotValue) {
                lesserElements.Add(point);
            }
            else {
                greaterElements.Add(point);
            }
        }

        //Final position of the pivot value in the sorted list.
        int pivotPosition = lesserElements.Count + _startIndex;

        //Check if pivot lies where we expect the median lie in the sorted list.
        if (pivotPosition == _midPoint) {
            result = pivot;
        }
        else {
            //Decide which elements to further inspect.
            result = pivotPosition < _midPoint ? 
                   FindMedianPoint(greaterElements, _midPoint, pivotPosition + 1, _axis) : 
                   FindMedianPoint( lesserElements, _midPoint,       _startIndex, _axis) ;
        }
        
        return result;
    }
    
    private void CreateChildNodes(BSPNode _node, List<Point> _points, int _axis) {
        Vector3 position = _node.m_point.m_position;

        List<Point> leftChildPoints  = new();
        List<Point> rightChildPoints = new();

        foreach (var point in _points) {
            if (point.m_position[_axis] < position[_axis]) {
                leftChildPoints.Add(point);
            }
            else if (point.m_position[_axis] > position[_axis]) {
                rightChildPoints.Add(point);
            }
            else {
                leftChildPoints.Add(point);
                rightChildPoints.Add(point);
            }
        }

        int childAxis = (_axis + 1) % 3;

        m_tree.Add(_node);

        if (leftChildPoints.Count > 0) {
            BSPNode leftChild = new(FindMedianPoint(leftChildPoints, leftChildPoints.Count / 2, 0, childAxis), _node.m_depth + 1);
            leftChildPoints.Remove(leftChild.m_point);

            _node.m_leftChild = leftChild;
            CreateChildNodes(leftChild, leftChildPoints, childAxis);
        }
        if (rightChildPoints.Count > 0) {
            BSPNode rightChild = new(FindMedianPoint(rightChildPoints, rightChildPoints.Count / 2, 0, childAxis), _node.m_depth + 1);
            rightChildPoints.Remove(rightChild.m_point);

            _node.m_rightChild = rightChild;
            CreateChildNodes(rightChild, rightChildPoints, childAxis);
        }
    }
}