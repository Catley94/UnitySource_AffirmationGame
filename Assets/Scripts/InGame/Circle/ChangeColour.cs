using System.Collections;
using System.Collections.Generic;
using TheraBytes.BetterUi;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public void SetColour(Color colour)
    {
        GetComponent<BetterImage>().color = new Color(colour.r, colour.g, colour.b, 1f);
    }
}
