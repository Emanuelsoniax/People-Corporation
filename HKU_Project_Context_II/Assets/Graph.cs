using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private RectTransform graphContainter;
    [SerializeField]
    private Sprite pointSprite;
    [SerializeField]
    private float pointSize;
    [SerializeField]
    private Color pointColor;
    [SerializeField]
    private Color connectionColor;

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;


    private List<GameObject> currentGraph = new List<GameObject>();
    private List<GameObject> currentGraphConnections = new List<GameObject>();
    private List<float> valueList = new List<float>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateGraph();
        }
    }

    public void AddValue(float _value)
    {
        if (valueList.Count >= 10)
        {
            valueList.Remove(valueList[0]);
        }

        valueList.Add(_value);
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        ResetGraph();
        DrawPoints(valueList);
        DrawConnections();
    }

    private void DrawPoints(List<float> _valueList)
    {
        float graphHeight = graphContainter.sizeDelta.y;
        float graphWidth = graphContainter.sizeDelta.x;

        for (int i = 0; i < _valueList.Count; i++)
        {
            float xPos = (i/ xMax) * graphWidth;
            float yPos = (valueList[i] / yMax) * graphHeight;
            CreatePoint(new Vector2(xPos, yPos));
        }
    }

    private void DrawConnections()
    {
        for (int i = 0; i < currentGraph.Count; i++)
        {
            if (i == currentGraph.Count-1)
            {
                continue;
            }
            else
            {
                ConnectPoints(currentGraph[i].GetComponent<RectTransform>().anchoredPosition, currentGraph[i + 1].GetComponent<RectTransform>().anchoredPosition);
            }
        }
    }

    private void CreatePoint(Vector2 _anchoredPos)
    {
        GameObject point = new GameObject("point", typeof(Image));
        point.transform.SetParent(graphContainter, false);

        RectTransform rectTransform = point.GetComponent<RectTransform>();
        point.GetComponent<Image>().color = pointColor;
        rectTransform.anchoredPosition = _anchoredPos;
        rectTransform.sizeDelta = new Vector2(pointSize, pointSize);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);

        currentGraph.Add(point);
    }

    private void ConnectPoints(Vector2 pointA, Vector2 pointB)
    {
        float distance = Vector2.Distance(pointA, pointB);
        Vector2 dir = (pointB - pointA).normalized;

        GameObject connection = new GameObject("dotConnection", typeof(Image));
        connection.transform.SetParent(graphContainter, false);
        connection.GetComponent<Image>().color = connectionColor;
        RectTransform rectTransform = connection.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta= new Vector2(distance, pointSize);

        rectTransform.anchoredPosition = pointA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));

        currentGraphConnections.Add(connection);

    }

    float GetAngleFromVectorFloat(Vector2 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void ResetGraph()
    {
        foreach (GameObject _point in currentGraph)
        {
            Destroy(_point);
        }
        currentGraph.Clear();

        foreach (GameObject _connection in currentGraphConnections)
        {
            Destroy(_connection);
        }
        currentGraphConnections.Clear();
    }
}
