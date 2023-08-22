using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AffirmationManager : MonoBehaviour
{
    
    [SerializeField] private SOAffirmation chosenAffirmation;
    [SerializeField] private int textMaxScale = 1;
    [SerializeField] private float transitionDelay = 0.5f;
    
    private TMP_Text affirmationTextObj;
    private string[] individualWords;
    private string chosenWord = "";
    private int currentWordIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetScaleToZero();

        SubToEvents();

        //Subscribe to game phase change, then change delay?
        
        affirmationTextObj = GetComponent<TMP_Text>();
        
        individualWords = chosenAffirmation.affirmations.Split(' ');

        StartCoroutine(LoopAffirmationAnimation());
    }

    private void SubToEvents()
    {
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase0.AddListener((delay) => SetTransitionDelay(delay * 2));
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase1.AddListener((delay) => SetTransitionDelay(delay * 2));
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase2.AddListener((delay) => SetTransitionDelay(delay * 2));
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase3.AddListener((delay) => SetTransitionDelay(delay * 2));
    }

    private void SetTransitionDelay(float delay)
    {
        transitionDelay = delay;
    }

    private IEnumerator LoopAffirmationAnimation()
    {
        while (true)
        {
            ChangeAffirmationWord();
            
            ScaleUpText();
            
            yield return new WaitForSeconds(transitionDelay);
            
            ScaleDownText();
            
            yield return new WaitForSeconds(transitionDelay);
        }

    }
    
    private void ChangeAffirmationWord()
    {
        for (int i = 0; i < individualWords.Length - 1; i++)
        {
            chosenWord = individualWords[currentWordIndex].ToUpper();
        }
        
        //Need a counter to loop through words
        affirmationTextObj.text = chosenWord; // Loop through words
        if (currentWordIndex < individualWords.Length - 1)
        {
            currentWordIndex++;
        }
        else
        {
            currentWordIndex = 0;
        }
    }

    private void ScaleDownText()
    {
        //May change duration, start fast (dep on game phase), then slow down
        transform.DOScale(0, transitionDelay).SetEase(Ease.Linear);
    }

    private void ScaleUpText()
    {
        //May change duration, start fast (dep on game phase), then slow down
        transform.DOScale(textMaxScale, transitionDelay).SetEase(Ease.Linear);
    }

    private void ResetScaleToZero()
    {
        transform.localScale = Vector3.zero;
    }
}
