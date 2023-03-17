using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum DocumentStatus {Unstamped, Declined, Approved}

[System.Serializable]

public class Document : Dragable, IStampable
{
    [Header("Document Settings")]
    public NewsContent newsContent;
    public DocumentStatus docStatus;
    [SerializeField]
    public DocumentText documentText;
    [SerializeField]
    public DocValues approvedValues;
    [SerializeField]
    public DocValues declinedValues;

    [Header("Sprites")]
    [SerializeField]
    private Image stampImage;
    [SerializeField]
    private Sprite declinedSprite;
    [SerializeField]
    private Sprite approvedSprite;


    private void Start()
    {
        
        UpdateStatus();
        UpdateText();
    }

    private void UpdateText()
    {
        documentText.titleText.text = documentText.documentTitle;
        documentText.descriptionText.text = documentText.documentDescription;
    }

    private void UpdateStatus()
    {
        switch (docStatus)
        {
            case DocumentStatus.Unstamped:
                stampImage.sprite = null;
                stampImage.color = Color.clear;
                return;
            case DocumentStatus.Declined:
                stampImage.sprite = declinedSprite;
                stampImage.color = Color.white;
                return;
            case DocumentStatus.Approved:
                stampImage.sprite = approvedSprite;
                stampImage.color = Color.white;
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

    [System.Serializable]
    public class DocumentText
    {
        [Tooltip ("The title of the document")]
        public string documentTitle;
        [Tooltip ("The text for the document")]
        public string documentDescription;
        [Tooltip("UI element in which the title will be displayed")]
        public TextMeshProUGUI titleText;
        [Tooltip("UI element in which the text will be displayed")]
        public TextMeshProUGUI descriptionText;
    }

    [System.Serializable]
    public class DocValues
    {
        public float land;
        public float sea;
        public float sky;
        public float peopleHappiness;
        public float companyReputation;
        public float companyEconomy;
        public float playerConciousness;
        public float companyIncome;

        public void ApplyValues()
        {
            Manager manager = GameObject.FindObjectOfType<Manager>();
            manager.stats.CompanyEconomy += companyEconomy;
            manager.stats.CompanyIncome += companyIncome;
            manager.stats.CompanyReputation += companyReputation;
            manager.stats.PlayerConciousness += playerConciousness;
            manager.stats.Land += land;
            manager.stats.Sea += sea;
            manager.stats.Sky += sky;
            manager.stats.PeopleHappiness += peopleHappiness;

            Debug.Log("applied changes");
        }
    }
}


