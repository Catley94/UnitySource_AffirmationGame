using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchPosition : MonoBehaviour
{

    private CircleCollider2D circleCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (!circleCollider.enabled)
            {
                circleCollider.enabled = true;
            }
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            transform.position = touchPosition;
        }
        else
        {
            if (circleCollider.enabled)
            {
                circleCollider.enabled = false;
            }
            if(Vector2.Distance(transform.position, new Vector2(-1000f, -1000f)) < 0f || 
               Vector2.Distance(transform.position, new Vector2(-1000f, -1000f)) > 0f) 
                transform.position = new Vector2(-1000f, -1000f);
        }
    }
}
