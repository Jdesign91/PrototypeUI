using System;
using UnityEngine;
using UnityEngine.Events;

public class SwipeManager : MonoBehaviour
{
    public float swipeThreshold = 40f;
    public float timeThreshold = 0.3f;

    public delegate void OnSwipeLeft();
    public OnSwipeLeft onSwipeLeft;

    private Vector2 _fingerDown;
    private DateTime _fingerDownTime;
    private Vector2 _fingerUp;
    private DateTime _fingerUpTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _fingerDown = Input.mousePosition;
            _fingerUp = Input.mousePosition;
            _fingerDownTime = DateTime.Now;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _fingerDown = Input.mousePosition;
            _fingerUpTime = DateTime.Now;
            CheckSwipe();
        }

        foreach (var touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                _fingerDown = touch.position;
                _fingerUp = touch.position;
                _fingerDownTime = DateTime.Now;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                _fingerDown = touch.position;
                _fingerUpTime = DateTime.Now;
                CheckSwipe();
            }
        }
    }

    private void CheckSwipe()
    {
        float duration = (float)_fingerUpTime.Subtract(_fingerDownTime).TotalSeconds;
        if (duration < timeThreshold)
        {
            return;
        }

        // Just keeping the calculation simple. Probably wise to do something like get the angle/direction of the swipe
        // and use that to determine what direction you REALLY went...
        if (_fingerUp.x < _fingerDown.x)
        {
            if (onSwipeLeft != null)
            {
                onSwipeLeft();
            }
        }
    }
}