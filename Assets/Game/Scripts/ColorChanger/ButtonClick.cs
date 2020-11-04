using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnButtonDown();
    public delegate void OnButtonUp();
    public OnButtonDown onButtonDown;
    public OnButtonUp onButtonUp;

    private bool _isBeingClicked = false;

    public bool IsClicked { get { return _isBeingClicked; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDown();
    }

    public void ButtonDown()
    {
        _isBeingClicked = true;
        onButtonDown?.Invoke();
    }

    public void ButtonUp()
    {
        _isBeingClicked = false;
        onButtonUp?.Invoke();
    }

    public void OnPointerUp(PointerEventData data)
    {
        ButtonUp();
    }
}
