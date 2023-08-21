using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircles : MonoBehaviour
{

    [SerializeField] private SOGrowDuration soGrowDuration;
    
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private float spawnDelay; 
    
    private int growDuration = 0;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnCircle();
        StartCoroutine(SpawnNewCircles());
    }

    // Update is called once per frame
    void Update()
    {
        
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

        UpdateSpawnDelay();
        
        if (parentCanvas != null && circlePrefab != null)
        {
            // Instantiate the image prefab
            GameObject circle = Instantiate(circlePrefab);
            

            // circle.GetComponent<Scale>().FullSize.AddListener(() =>
            // {
            //     Invoke(nameof(SpawnCircle), spawnDelay);
            //     circle.GetComponent<Scale>().FullSize.RemoveAllListeners();
            //     
            // });

            int gamePhase = GetGamePhase();
            
            circle.GetComponent<Scale>().SetGrowDuration(soGrowDuration.growPhases[gamePhase]);
            Debug.Log("Game phase: " + gamePhase);
            Debug.Log("Setting grow duration to: " + soGrowDuration.growPhases[gamePhase]);

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

    private void UpdateSpawnDelay()
    {
        
        SetSpawnDelay(soGrowDuration.growPhases[GetGamePhase()]);
    }

    private int GetGamePhase()
    {
        return GameObject.Find("GameManager").GetComponent<State_Phase>().GetCurrentPhase();
    }

    private void SetSpawnDelay(float delay)
    {
        if (spawnDelay != delay)
        {
            Debug.Log($"Setting delay to: {delay}");
            spawnDelay = delay;
        }
    }
}
