using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PhaseObject
{
    public float growDuration;
    public float delayDuration;
    
public PhaseObject(float growDuration, float delayDuration)
    {
        this.growDuration = growDuration;
        this.delayDuration = delayDuration;
    }
}

public class State_Phase : MonoBehaviour
{

    [FormerlySerializedAs("soGrowDuration")] [SerializeField] private SOCircleConfig soCircleConfig;

    [HideInInspector]public UnityEvent<PhaseObject> Phase0;
    [HideInInspector]public UnityEvent<PhaseObject> Phase1;
    private bool phase1Complete = false;
    [HideInInspector]public UnityEvent<PhaseObject> Phase2;
    private bool phase2Complete = false;
    [HideInInspector]public UnityEvent<PhaseObject> Phase3;
    private bool phase3Complete = false;

    [SerializeField] private int currentPhase = 0;
    [SerializeField] private int minutes = 0;
    [SerializeField] private int seconds = 0;
    
    private DateTime initialTime;
    private DateTime currentTime;
    private TimeSpan elapsedTime;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        Phase0?.Invoke(new PhaseObject(soCircleConfig.growPhases[GetCurrentPhase()], soCircleConfig.spawnDelayTimeoutPhases[GetCurrentPhase()]));
        
        
        
        //Get initial time of when script started (When game started)
        initialTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        //Get elapsed time to work out phases
        elapsedTime = DateTime.Now - initialTime;
        minutes = elapsedTime.Minutes;
        seconds = elapsedTime.Seconds;
        // Debug.Log(elapsedTime.Minutes + ":" + elapsedTime.Seconds);
        
        if(elapsedTime.Minutes != currentPhase) Debug.Log(elapsedTime.Minutes + ":" + elapsedTime.Seconds);
        UpdatePhase();
    }
    
    private void UpdatePhase()
    {
        if (!phase1Complete)
        {
            if (elapsedTime.TotalMinutes >= soCircleConfig.phaseMinuteThresholds[0])
            {
                currentPhase = 1;
                Phase1?.Invoke(new PhaseObject(soCircleConfig.growPhases[GetCurrentPhase()], soCircleConfig.spawnDelayTimeoutPhases[GetCurrentPhase()]));
                phase1Complete = true;
            }
        }
        else if (!phase2Complete)
        {
            if (elapsedTime.TotalMinutes >= soCircleConfig.phaseMinuteThresholds[1])
            {
                currentPhase = 2;
                Phase2?.Invoke(new PhaseObject(soCircleConfig.growPhases[GetCurrentPhase()], soCircleConfig.spawnDelayTimeoutPhases[GetCurrentPhase()]));
                phase2Complete = true;
            }
        }
        else if (!phase3Complete)
        {
            if (elapsedTime.TotalMinutes >= soCircleConfig.phaseMinuteThresholds[2])
            {
                currentPhase = 3;
                Phase3?.Invoke(new PhaseObject(soCircleConfig.growPhases[GetCurrentPhase()], soCircleConfig.spawnDelayTimeoutPhases[GetCurrentPhase()]));
                phase3Complete = true;
            }
        }
    }
    
    public int GetCurrentPhase()
    {
        return currentPhase;
    }
}
