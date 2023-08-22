using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToInGame : MonoBehaviour
{
    public void LoadInGameScene(SOAffirmation chosenAffirmation)
    {
        ES3.Save("ChosenAffirmation", chosenAffirmation);
        SceneManager.LoadScene("InGame");
    }
}
