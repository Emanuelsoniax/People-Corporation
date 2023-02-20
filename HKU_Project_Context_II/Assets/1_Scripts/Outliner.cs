using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner: MonoBehaviour
{
    [Header ("Outline Settings")]
    [SerializeField]
    private bool showOutline;
    [SerializeField][Range (0, 0.5f)]
    private float outlineThickness;
    [SerializeField]
    private Color outlineColor;

    private void Start()
    {
        GetComponent<SpriteRenderer>().material.SetColor("_Color", outlineColor);
        //GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", outlineThickness);
    }

    public void OnMouseEnter()
    {
        Debug.Log("Show Outline");
        if (showOutline)
        {
            GetComponent<SpriteRenderer>().material.SetFloat("_OutlinesOn", 1f);
        }
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_OutlinesOn", 0f);
    }

    private void OnMouseDrag()
    {
        GetComponent<SpriteRenderer>().material.SetFloat("_OutlinesOn", 0f);
    }
}
