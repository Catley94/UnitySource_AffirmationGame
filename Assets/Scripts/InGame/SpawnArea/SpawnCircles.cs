using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnCircles : MonoBehaviour
{

    [FormerlySerializedAs("soGrowDuration")] [SerializeField] private SOCircleConfig soCircleConfig;
    
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnDelay; 
    
    private int currentPhase = 0;
    
    private int growDuration = 0;
    private float delayDuration = 0f;

    private void OnEnable()
    {
        SubToEvents();
    }

    // Start is called before the first frame update
    void Start()
    {
        // SpawnCircle();
        StartCoroutine(SpawnNewCircles());

        

    }

    private void SubToEvents()
    {
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase0.AddListener((phaseObj) =>
        {
            currentPhase = 0;
            delayDuration = phaseObj.delayDuration;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase1.AddListener((phaseObj) =>
        {
            currentPhase = 1;
            delayDuration = phaseObj.delayDuration;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase2.AddListener((phaseObj) =>
        {
            currentPhase = 2;
            delayDuration = phaseObj.delayDuration;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase3.AddListener((phaseObj) =>
        {
            currentPhase = 3;
            delayDuration = phaseObj.delayDuration;
            SetSpawnDelay(phaseObj.delayDuration);
        });
    }

    private IEnumerator SpawnNewCircles()
    {
        while (true)
        {
            SpawnCircle();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnCircle()
    {

        if (parentCanvas != null && circlePrefab != null)
        {
            // Instantiate the image prefab
            GameObject circle = Instantiate(circlePrefab);

            circle.GetComponent<Scale>().SetGrowDuration(soCircleConfig.growPhases[currentPhase]);
            circle.GetComponent<OnTouch>().SetTimeOut(soCircleConfig.spawnDelayTimeoutPhases[currentPhase]);
            
            Debug.Log("Game phase: " + currentPhase);
            Debug.Log("Setting grow duration to: " + soCircleConfig.growPhases[currentPhase]);

            // Set the parent to the canvas
            circle.transform.SetParent(transform, false);

            // Get the RectTransform of the image
            RectTransform imageRect = circle.GetComponent<RectTransform>();

            // Calculate canvas bounds
            RectTransform canvasRect = GetComponent<RectTransform>();
            float canvasWidth = canvasRect.rect.width * 0.9f;
            float canvasHeight = canvasRect.rect.height * 0.9f;

            // Calculate random position within canvas bounds
            float randomX = Random.Range(-canvasWidth / 2f, canvasWidth / 2f);
            float randomY = Random.Range(-canvasHeight / 2f, canvasHeight / 2f);

            // Set the position within the canvas bounds
            imageRect.anchoredPosition = new Vector2(randomX, randomY);
        }
        else
        {
            Debug.LogWarning("Parent canvas or image prefab not assigned.");
        }
        
    }

    // private void UpdateSpawnDelay()
    // {
    //     
    //     SetSpawnDelay(soGrowDuration.growPhases[GetGamePhase()]);
    // }

    // private int GetGamePhase()
    // {
    //     return GameObject.Find("GameManager").GetComponent<State_Phase>().GetCurrentPhase();
    // }

    private void SetSpawnDelay(float delay)
    {
        if (spawnDelay != delay)
        {
            Debug.Log($"Setting delay to: {delay}");
            spawnDelay = delay;
        }
    }
}
