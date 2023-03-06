using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField]
    public Stats stats = new Stats();
    
    public enum GamePhase { MainMenu =0, Desktop=1, Workday=2, EndWorkday=3}
    public enum GameStatus { Play, Pause}
    [Header ("Game States")]
    public GamePhase gamePhase;
    public GameStatus gameStatus;

    [Header("Printer")]
    [SerializeField]
    private Printer printer;

    [Header("Menu's")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject pauzeMenu;

    [Header("Camera")]
    [SerializeField]
    private CameraMovement cam;

    [Header("Clock")]
    [SerializeField]
    private Clock clock = new Clock();
    [SerializeField]
    private TextMeshProUGUI timeDisplay;



    private void Start()
    {
        cam.MoveToDesktop();
        stats.UpdateUI();
        SwitchPhase(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause();
        }
        timeDisplay.text = clock.CurrentTime;
        UpdateGame();
    }

    private void UpdateGame()
    {
        switch (gamePhase)
        {
            case GamePhase.MainMenu:
                return;

            case GamePhase.Desktop:
                return;

            case GamePhase.Workday:
                clock.Tick();
                printer.Print();
                CheckConditions();
                return;

            case GamePhase.EndWorkday:
                //stop time
                //display UI
                return;
        }
    }

    private void CheckConditions()
    {
        if (clock.currentTime >= (540 + 480))
        {
            SwitchPhase(3);
        }

        if(stats.PlayerConciousness >= 1 || stats.EarthStats <= 0 || printer.cardDeck.Count == 0 && FindObjectsOfType<Document>().Length == 0 || stats.CompanyStats <=0)
        {
            SwitchPhase(3);
        }
    }
    
    public void Resume()
    {
        gameStatus = GameStatus.Play;
        //remove UI
        pauzeMenu.SetActive(false);
        Time.timeScale = 1;
        cam.cameraLocked = false;
    }

    public void Pause()
    {
        gameStatus = GameStatus.Pause;
        //display UI
        pauzeMenu.SetActive(true);
        Time.timeScale = 0;
        cam.cameraLocked = true;
    }

    public void SwitchPhase(int _phase)
    {
        //set phase to MainMenu
        if (_phase == 0)
        {
            mainMenu.SetActive(true);
            gamePhase = GamePhase.MainMenu;
        }

        //set phase to Desktop
        if (_phase == 1)
        {
            StartCoroutine(cam.MoveToDesktop());
            cam.cameraLocked = true;
            //reset time to start workday
            clock.currentTime = 540;
            //displayUI
            gamePhase = GamePhase.Desktop;
        }

        //set phase to Workday
        if (_phase == 2)
        {
            StartCoroutine(cam.MoveToDesk());
            gamePhase = GamePhase.Workday;
            clock.Start();
        }

        //set phase to EndWorkday
        if (_phase == 3)
        {
            //displayUI
            gamePhase = GamePhase.EndWorkday;
            StartCoroutine(cam.MoveToDesktop());
            cam.cameraLocked = true;
        }
    }
}

[System.Serializable]
public class Stats
{
    [Header("Eath Stats")]
    [SerializeField]
    private float land;
    public float Land
    {
        get { return land; }
        set { land = value;
            if (land < 0)
            {
                land = 0;
            }
            if (land > 1)
            {
                land = 1;
            }
        }
    }
    [SerializeField]
    private float sea;
    public float Sea
    {
        get { return sea; }
        set { sea = value;
            if (sea < 0)
            {
                sea = 0;
            }
            if (sea > 1)
            {
                sea = 1;
            }
        }
    }
    [SerializeField]
    private float sky;
    public float Sky
    {
        get { return sky; }
        set { sky = value;
            if (sky < 0)
            {
                sky = 0;
            }
            if (sky > 1)
            {
                sky = 1;
            }
        }
    }
    [SerializeField]
    private float peopleHappiness;
    public float PeopleHappiness
    {
        get { return peopleHappiness; }
        set
        {
            peopleHappiness = value;
            if (peopleHappiness < 0)
            {
                peopleHappiness = 0;
            }
            if (peopleHappiness > 1)
            {
                peopleHappiness = 1;
            }
        }
    }
    [SerializeField]
    private float earthStats;
    public float EarthStats
    {
        get { return earthStats = PeopleHappiness + (Land + Sea + Sky); }
        set { earthStats = value;
            if (earthStats < 0)
            {
                earthStats = 0;
            }
            if (earthStats > 4)
            {
                earthStats = 4;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private Slider earthSlider;

    [Header ("Company Stats")]
    [SerializeField]
    private float companyReputation;
    public float CompanyReputation
    {
        get { return companyReputation; }
        set { companyReputation = value;
            if (companyReputation < 0)
            {
                companyReputation = 0;
            }
            if (companyReputation > 1)
            {
                companyReputation = 1;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private float companyEconomy;
    public float CompanyEconomy
    {
        get { return companyEconomy; }
        set { companyEconomy = value;
            if (companyEconomy < 0)
            {
                companyEconomy = 0;
            }
            if (companyEconomy > 1)
            {
                companyEconomy = 1;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private float companyStats;
    public float CompanyStats
    {
        get { companyStats = CompanyReputation + CompanyEconomy;
              return companyStats; }
        set { companyStats = value;
            if (companyStats < 0)
            {
                companyStats = 0;
            }
            if (companyStats > 2)
            {
                companyStats = 2;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private Slider companySlider;


    [Header("Player Stats")]
    [SerializeField]
    private float playerConciousness;
    public float PlayerConciousness
    {
        get { return playerConciousness; }
        set { playerConciousness = value;
            if (playerConciousness < 0)
            {
                playerConciousness = 0;
            }
            if (playerConciousness > 1)
            {
                playerConciousness = 1;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private Slider conciousnessSlider;

    [SerializeField]
    private float companyIncome;
    public float CompanyIncome
    {
        get { return companyIncome; }
        set { companyIncome = value;
            if (companyIncome < 0)
            {
                companyIncome = 0;
            }
            if (companyIncome > 1)
            {
                companyIncome = 1;
            }
            UpdateUI();
            }
    }
    [SerializeField]
    private Slider incomeSlider;

    public void UpdateUI()
    {
        earthSlider.value = EarthStats;
        companySlider.value = CompanyStats;
        conciousnessSlider.value = PlayerConciousness;
        incomeSlider.value = CompanyIncome;
    }
}
