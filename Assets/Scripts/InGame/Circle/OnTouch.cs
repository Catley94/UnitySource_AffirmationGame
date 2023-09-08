using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
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
    void Start()
    {
        circleImage = GetComponent<Image>();

        audioSource = GameObject.FindWithTag("AudioSource").GetComponent<AudioSource>();
        
        Invoke(nameof(TimedOut), timeLeft);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Touched();
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
        TimedOutEvent?.Invoke();
    }
    
    private void RemoveFromCanvas()
    {
        Destroy(gameObject);
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
