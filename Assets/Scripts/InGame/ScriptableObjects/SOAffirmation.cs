using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Affirmation", menuName = "Affirmations/New Affirmation", order = 1)]
public class SOAffirmation : ScriptableObject
{
    public string affirmationName;
    [TextArea(3, 10)] public string affirmations;
}

