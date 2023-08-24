using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnCircles : MonoBehaviour
{

    [FormerlySerializedAs("soGrowDuration")] [SerializeField] private SOCircleConfig soCircleConfig;
    
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnDelay; 
    
    private IObjectPool<GameObject> circlePool;

    private int currentPhase = 0;
    
    private int growDuration = 0;
    private Color currentColour;

    private void OnEnable()
    {
        SubToEvents();
    }

    // Start is called before the first frame update
    void Start()
    {
        // SpawnCircle();
        circlePool = new ObjectPool<GameObject>(OnCreate, OnGet, OnRelease, OnDestroyCircle, false, 5, 5);
        StartCoroutine(SpawnNewCircles());
    }

    

    private void SubToEvents()
    {
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase0.AddListener((phaseObj) =>
        {
            currentPhase = 0;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase1.AddListener((phaseObj) =>
        {
            currentPhase = 1;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase2.AddListener((phaseObj) =>
        {
            currentPhase = 2;
            SetSpawnDelay(phaseObj.delayDuration);
        });
        
        GameObject.Find("GameManager").GetComponent<State_Phase>().Phase3.AddListener((phaseObj) =>
        {
            currentPhase = 3;
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
            GameObject circle = circlePool.Get();

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
    
    private GameObject OnCreate()
    {
        GameObject circle = Instantiate(circlePrefab);
        circle.GetComponent<Pool>().SetObjectPool(circlePool);
        return circle;
    }
    
    private void OnGet(GameObject circle)
    {
        circle.GetComponent<Scale>().SetGrowDuration(soCircleConfig.growPhases[currentPhase]);
        circle.GetComponent<OnTouch>().SetTimeOut(soCircleConfig.spawnDelayTimeoutPhasesInSeconds[currentPhase]);
        circle.GetComponent<ChangeColour>().SetColour(soCircleConfig.circleColourPhases[currentPhase]);
        circle.transform.SetParent(transform, false);
        circle.SetActive(true);
    }
    
    private void OnRelease(GameObject circle)
    {
        circle.GetComponent<Scale>().SetGrowDuration(0f);
        circle.GetComponent<OnTouch>().SetTimeOut(0f);
        circle.GetComponent<ChangeColour>().SetColour(new Color(0f,0f,0f));
        circle.SetActive(false);
    }
    
    private void OnDestroyCircle(GameObject circle)
    {
        Destroy(circle);
    }
    
}
