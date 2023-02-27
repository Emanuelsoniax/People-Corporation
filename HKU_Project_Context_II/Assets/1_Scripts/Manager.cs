using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Stats stats;
    
    public enum GamePhase { MainMenu =0, Desktop=1, Workday=2, EndWorkday=3}
    public enum GameStatus { Play, Pause}
    [Header ("Game States")]
    public GamePhase gamePhase;
    public GameStatus gameStatus;

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
                //spawnDocuments
                return;

            case GamePhase.EndWorkday:
                //stop time
                //display UI
                return;
        }
    }
    
    public void Resume()
    {
        gameStatus = GameStatus.Play;
        //remove UI
        pauzeMenu.SetActive(false);
    }

    public void Pause()
    {
        gameStatus = GameStatus.Pause;
        //display UI
        pauzeMenu.SetActive(true);
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
            //stop time
            //displayUI
            cam.MoveToDesktop();
            cam.cameraLocked = true;
            gamePhase = GamePhase.EndWorkday;
        }
    }
}

[System.Serializable]
public class Stats
{
    [Header ("Eath Stats")]
    [SerializeField]
    private float earthStats;
    public float EarthStats
    {
        get { return earthStats; }
        set { earthStats = value;
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
            UpdateUI();
            }
    }
    [SerializeField]
    private float companyEconomy;
    public float CompanyEconomy
    {
        get { return companyEconomy; }
        set { companyEconomy = value;
              UpdateUI();
            }
    }
    [SerializeField]
    private float companyStats;
    public float CompanyStats
    {
        get { companyStats = companyReputation + companyEconomy;
              return companyStats; }
        set { companyStats = value;
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
              UpdateUI();
            }
    }
    [SerializeField]
    private Slider conciousnessSlider;

    [SerializeField]
    private float dailyIncome;
    public float DailyIncome
    {
        get { return dailyIncome; }
        set { dailyIncome = value;
              UpdateUI();
            }
    }


    public void UpdateUI()
    {
        earthSlider.normalizedValue = EarthStats;
        companySlider.normalizedValue = CompanyStats;
        conciousnessSlider.normalizedValue = PlayerConciousness;
    }
}
