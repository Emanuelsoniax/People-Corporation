using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Loop : MonoBehaviour
{
    [Header("Stamp settings")]
    [SerializeField]
    private StampType stampType;
    [Tooltip("The distance in which a Stampable will be able to be stamped")]
    public float snapDistanceX;
    [Tooltip("The distance in which a Stampable will be able to be stamped")]
    public float snapDistanceY;
    
    [Header ("Display")]
    [SerializeField]
    private GameObject zoomCanvas;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Image land;
    [SerializeField]
    private Image sea;
    [SerializeField]
    private Image sky;
    [SerializeField]
    private Image income;


    [Header("Audio")]
    [SerializeField]
    [Tooltip("The AudioSource that plays the Dragable SFX")]
    private AudioSource audioSource;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is picked up")]
    private AudioClip pickup;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is dropped")]
    private AudioClip dropped;
    [SerializeField]
    [Tooltip("The AudioClip that's played when the Dragable is zooming")]
    private AudioClip zoom;

    [Header("Gizmos")]
    [SerializeField]
    private bool drawGizmos;

    private Vector2 offset;
    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
    }

    private Document CheckForStampable()
    {
        IStampable[] stampables = FindObjectsOfType<Document>();
        foreach (Document _stampable in stampables)
        {
            if (Mathf.Abs(transform.position.x - _stampable.transform.position.x) < snapDistanceX / 2 &&
                    Mathf.Abs(transform.position.y - _stampable.transform.position.y) < snapDistanceY / 2)
            {
                return _stampable;
            }
            continue;
        }
        return null;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() - offset;
    }

    private void OnMouseUp()
    {

        if (CheckForStampable() != null && CheckForStampable().docStatus == DocumentStatus.Unstamped)
        {
            Zoom(CheckForStampable());
            audioSource.PlayOneShot(zoom);
            Invoke("ReturnToOriginalPos", 0.5f);
        }
        else
        {
            ReturnToOriginalPos();
        }
        audioSource.PlayOneShot(dropped);
    }

    private void Zoom(Document _document)
    {
        zoomCanvas.SetActive(true);

        titleText.text = _document.documentText.documentTitle;
        descriptionText.text = _document.documentText.documentDescription;

        ConfiqureIcons(_document);
    }

    private void ConfiqureIcons(Document document)
    {
        if (document.approvedValues.land >= 0)
        {
            land.color = Color.green;
        } else land.color = Color.red;

        if (document.approvedValues.sky >= 0)
        {
            sky.color = Color.green;
        }
        else sky.color = Color.red;

        if (document.approvedValues.sea >= 0)
        {
            sea.color = Color.green;
        }
        else sea.color = Color.red;

        if (document.approvedValues.companyIncome >= 0)
        {
            income.color = Color.green;
        }
        else income.color = Color.red;
    }

    private void ReturnToOriginalPos()
    {
        float timeElapsed = 0;
        while (timeElapsed < 4)
        {
            transform.position = Vector2.Lerp(transform.position, originalPos, timeElapsed / 4);
            timeElapsed += Time.deltaTime;
        }
        transform.position = originalPos;
    }

    private void OnMouseDown()
    {
        audioSource.PlayOneShot(pickup);
        offset = GetMousePos() - (Vector2)transform.position;
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = new Color(1, 1, 1, 0.2f);
            Gizmos.DrawCube(transform.position, new Vector2(snapDistanceX, snapDistanceY));
        }
    }
}
