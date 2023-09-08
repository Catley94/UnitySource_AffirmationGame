using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GrowDurationConfig", menuName = "Config/New Duration Config", order = 1)]
public class SOCircleConfig : ScriptableObject
{
    [FormerlySerializedAs("phases")] public float[] growPhases;
    public float[] shrinkPhases;
    //spawnDelayTimeoutPhases[i] must be greater than or equal to growPhases[i]
    [FormerlySerializedAs("spawnDelayTimeoutPhases")] [FormerlySerializedAs("spawnDelayPhases")] public float[] spawnDelayTimeoutPhasesInSeconds;
    public float[] phaseMinuteThresholds;
    public float endGameTimeThreshold;
    public Color[] circleColourPhases;
    public float[] volumePhases;
}

