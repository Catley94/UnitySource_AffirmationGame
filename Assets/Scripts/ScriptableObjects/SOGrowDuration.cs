using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GrowDurationConfig", menuName = "Config/New Duration Config", order = 1)]
public class SOGrowDuration : ScriptableObject
{
    [FormerlySerializedAs("phases")] public float[] growPhases;
    public float[] shrinkPhases;
    public float[] spawnDelayPhases;
    public int[] phaseMinuteThresholds;
}

