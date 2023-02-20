using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Stats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats.UpdateUI();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            stats.CompanyEconomy += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            stats.CompanyReputation += 0.1f;
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
