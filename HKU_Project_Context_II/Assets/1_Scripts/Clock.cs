using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Clock
{
    public float currentTime = 540;
    public string CurrentTime
    {
        get { return TimeToString(); }
    }
    private bool isTicking = false;
    [SerializeField]
    private float timeBetweenTicks;
    [SerializeField]
    private float timeAddedEachTick;
    float timer;

    public void Tick()
    {
        if (isTicking)
        {
            timer += Time.deltaTime;
            if(timer >= timeBetweenTicks)
            {
                currentTime += timeAddedEachTick;
                timer = 0;
            }
        }
    }

    public void Start()
    {
        isTicking = true;
    }

    public void Stop()
    {
        isTicking = false;
    }

    public void ResetTime()
    {
        currentTime = 540;
    }

    private string TimeToString()
    {
        int intTime = (int)currentTime;
        int hours = intTime / 60;
        int minutes = intTime % 60;

        return hours.ToString("00") + ":" + minutes.ToString("00");
    }
}
