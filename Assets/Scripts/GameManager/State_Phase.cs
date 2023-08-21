using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class State_Phase : MonoBehaviour
{

    [SerializeField] private SOGrowDuration soGrowDuration;

    [HideInInspector]public UnityEvent Phase1;
    private bool phase1Complete = false;
    [HideInInspector]public UnityEvent Phase2;
    private bool phase2Complete = false;
    [HideInInspector]public UnityEvent Phase3;
    private bool phase3Complete = false;

    private int currentPhase = 0;
    
    private DateTime initialTime;
    private DateTime currentTime;
    private TimeSpan elapsedTime;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        //Get initial time of when script started (When game started)
        initialTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        //Get elapsed time to work out phases
        elapsedTime = DateTime.Now - initialTime;
        // Debug.Log(elapsedTime.Minutes + ":" + elapsedTime.Seconds);
        
        if(elapsedTime.Minutes != currentPhase) Debug.Log(elapsedTime.Minutes + ":" + elapsedTime.Seconds);
        UpdatePhase();
    }
    
    private void UpdatePhase()
    {
        if (!phase1Complete)
        {
            if (elapsedTime.TotalMinutes >= soGrowDuration.phaseMinuteThresholds[0])
            {
                Phase1?.Invoke();
                phase1Complete = true;
                currentPhase = 1;
            }
        }
        else if (!phase2Complete)
        {
            if (elapsedTime.TotalMinutes >= soGrowDuration.phaseMinuteThresholds[1])
            {
                Phase2?.Invoke();
                phase2Complete = true;
                currentPhase = 2;
            }
        }
        else if (!phase3Complete)
        {
            if (elapsedTime.TotalMinutes >= soGrowDuration.phaseMinuteThresholds[3])
            {
                Phase3?.Invoke();
                phase3Complete = true;
                currentPhase = 3;
            }
        }
    }
    
    public int GetCurrentPhase()
    {
        return currentPhase;
    }
}
