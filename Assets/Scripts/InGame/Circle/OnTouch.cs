using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.UI;

public class OnTouch : MonoBehaviour
{
    
    [SerializeField] private float timeLeft = 1f;
    [SerializeField] private AudioClip audioClip;
    private Image circleImage;
    private AudioSource audioSource;
    private bool touched = false;


    public UnityEvent TimedOutEvent;
    public UnityEvent TouchedEvent;
    
    

    // Start is called before the first frame update
    void OnEnable()
    {
        
        circleImage = GetComponent<Image>();

        audioSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
        
        Invoke(nameof(TimedOut), timeLeft);
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            
            if (IsPointInsideCircle(touchPosition, transform.position, circleImage.rectTransform.sizeDelta.x / 2f))
            {
                // The touch is inside the circle image
                Debug.Log("Touched inside the circle!");
                Touched();
            }
        }
    }

    private void Touched()
    {
        if (!touched)
        {
            touched = true;
            CancelInvoke(nameof(TimedOut));
            TouchedEvent?.Invoke();
            //TODO: Play sound - then Remove From Canvas;
            audioSource.PlayOneShot(audioClip);
            RemoveFromCanvas();
        }

    }
    
    private void TimedOut()
    {
        Debug.Log("TIMED OUT");
        TimedOutEvent?.Invoke();
    }
    
    private void RemoveFromCanvas()
    {
        // Destroy(gameObject);
        GetComponent<Pool>().GetObjectPool().Release(gameObject);
    }

    // Helper function to check if a point is inside a circle
    private bool IsPointInsideCircle(Vector3 point, Vector3 circleCenter, float radius)
    {
        float distanceSquared = (point - circleCenter).sqrMagnitude;
        return distanceSquared <= radius * radius;
    }

    public void SetTimeOut(float timeout)
    {
        timeLeft = timeout;
    }
}
