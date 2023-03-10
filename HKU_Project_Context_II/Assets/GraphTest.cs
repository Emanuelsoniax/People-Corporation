using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphTest : MonoBehaviour
{
    [SerializeField]
    private RectTransform graphContainter;
    [SerializeField]
    private Sprite pointSprite;
    [SerializeField]
    private float pointSize;

    private List<GameObject> currentGraph = new List<GameObject>();
    private List<float> valueList = new List<float>();

    private void Start()
    {
        AddValue(50);
        AddValue(3);
        AddValue(6);
        AddValue(20);
        AddValue(40);
        AddValue(25);
        AddValue(30);
        AddValue(20);
    }


    public void AddValue(float _value)
    {
        if (valueList.Count >= 6)
        {
            valueList.Remove(valueList[0]);
        }

        valueList.Add(_value);
        UpdateGraph();
    }

    private void UpdateGraph()
    {
        ResetGraph();
        DrawGraph(valueList);
    }

    private void DrawGraph(List<float> _valueList)
    {
        float graphHeight = graphContainter.sizeDelta.y;
        float yMaximum = 100f;
        float xBetweenPoints = 50f;

        for (int i = 0; i < _valueList.Count; i++)
        {
            float xPos = i * xBetweenPoints;
            float yPos = (valueList[i] / yMaximum) * graphHeight;
            CreatePoint(new Vector2(xPos, yPos));
        }

        for (int i = 0; i < currentGraph.Count; i++)
        {
            if(currentGraph[i] == currentGraph[currentGraph.Count])
            {
                return;
            }
            
            ConnectPoints(currentGraph[i].GetComponent<RectTransform>().anchoredPosition, currentGraph[i+1].GetComponent<RectTransform>().anchoredPosition)
        }
    }

    private void CreatePoint(Vector2 _anchoredPos)
    {
        GameObject point = new GameObject("point", typeof(Image));
        point.transform.SetParent(graphContainter, false);

        RectTransform rectTransform = point.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = _anchoredPos;
        rectTransform.sizeDelta = new Vector2(pointSize, pointSize);
        rectTransform.anchorMin = new Vector2(0,0);
        rectTransform.anchorMax = new Vector2(0,0);

        currentGraph.Add(point);
    }

    private void ConnectPoints(Vector2 pointA, Vector2 pointB)
    {
        GameObject connection = new GameObject("dotConnection", typeof(Image));

    }
    private void ResetGraph()
    {
        foreach (GameObject _point in currentGraph)
        {
            Destroy(_point);
        }
        currentGraph.Clear();
    }
}
