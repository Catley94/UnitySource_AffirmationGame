using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    [SerializeField] private float growDuration = 0f;
    [SerializeField] private float shrinkDuration = 0f;
    
    public UnityEvent FullSize;
    public Vector3 targetScale;
    
    private Image circleImage;
    private Vector3 initialScale;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<OnTouch>().TimedOutEvent.AddListener(() =>
        {
            StartCoroutine(Shrink());
            GetComponent<OnTouch>().TimedOutEvent.RemoveAllListeners();
        });
        
        GetComponent<OnTouch>().TouchedEvent.AddListener(() =>
        {
            StopCoroutine(Shrink());
            GetComponent<OnTouch>().TimedOutEvent.RemoveAllListeners();
        });
        
        SetupReferences();
        StartCoroutine(Grow());
        initialScale = circleImage.transform.localScale;
    }

    private void SetupReferences()
    {
        circleImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetGrowDuration(float duration)
    {
        growDuration = duration;
    }
    
    public void SetShrinkDuration(float duration)
    {
        shrinkDuration = duration;
    }

    private IEnumerator Grow()
    {
        float elapsedTime = 0f;

        while (elapsedTime < growDuration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / growDuration;

            // Use Mathf.Lerp to interpolate between the initial and target scales
            circleImage.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        circleImage.transform.localScale = targetScale;

        // Scaling is complete
        // You can perform additional actions here
        
        FullSize?.Invoke();
    }
    
    private IEnumerator Shrink()
    {
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            // Calculate the interpolation factor
            float t = elapsedTime / shrinkDuration;

            // Use Mathf.Lerp to interpolate between the initial and target scales
            circleImage.transform.localScale = Vector3.Lerp( targetScale, initialScale, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly the target scale
        circleImage.transform.localScale = targetScale;

        // Scaling is complete
        // You can perform additional actions here
        
        RemoveFromCanvas();
    }

    private void RemoveFromCanvas()
    {
        Destroy(gameObject);
    }
}
