using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GrowDurationConfig", menuName = "Config/New Duration Config", order = 1)]
public class SOCircleConfig : ScriptableObject
{
    [FormerlySerializedAs("phases")] public float[] growPhases;
    public float[] shrinkPhases;
    //spawnDelayTimeoutPhases[i] must be greater than growPhases[i]
    [FormerlySerializedAs("spawnDelayPhases")] public float[] spawnDelayTimeoutPhases;
    public float[] phaseMinuteThresholds;
}

