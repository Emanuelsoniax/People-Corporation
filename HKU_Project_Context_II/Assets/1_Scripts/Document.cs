using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DocumentStatus {Unstamped, Declined, Approved}

[System.Serializable]

public class Document : Dragable, IStampable
{
    [Header("Document Settings")]
    public DocumentStatus docStatus;

    [Header("Sprites")]
    [SerializeField]
    private Sprite unstampedSprite;
    [SerializeField]
    private Sprite declinedSprite;
    [SerializeField]
    private Sprite approvedSprite;

    private void Start()
    {
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        switch (docStatus)
        {
            case DocumentStatus.Unstamped:
                GetComponent<SpriteRenderer>().sprite = unstampedSprite;
                return;
            case DocumentStatus.Declined:
                GetComponent<SpriteRenderer>().sprite = declinedSprite;
                return;
            case DocumentStatus.Approved:
                GetComponent<SpriteRenderer>().sprite = approvedSprite;
                return;
        }
    }

    public void Stamp(StampType _stampType)
    {
        if(_stampType == StampType.Approve)
        {
            docStatus = DocumentStatus.Approved;
            UpdateStatus();
        }
        
        if(_stampType == StampType.Decline)
        {
            docStatus = DocumentStatus.Declined;
            UpdateStatus();
        }
    }
}
