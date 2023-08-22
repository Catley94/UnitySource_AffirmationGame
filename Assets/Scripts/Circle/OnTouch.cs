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
    private Image circleImage;

    public UnityEvent TimedOutEvent;
    public UnityEvent TouchedEvent;
    

    // Start is called before the first frame update
    void Start()
    {
        circleImage = GetComponent<Image>();
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
        CancelInvoke(nameof(TimedOut));
        TouchedEvent?.Invoke();
        //TODO: Play sound - then Remove From Canvas;
        RemoveFromCanvas();
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
