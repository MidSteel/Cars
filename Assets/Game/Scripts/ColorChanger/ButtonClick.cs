using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void OnButtonDown();
    public delegate void OnButtonUp();
    public OnButtonDown onButtonDown;
    public OnButtonUp onButtonUp;

    private bool _isBeingClicked = false;
    private Image _image = null;
    private Color _color = new Color();

    public bool IsClicked { get { return _isBeingClicked; } }

    private void Awake()
    {
        _image = this.GetComponent<Image>();
        _color = _image.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonDown();
    }

    public void ButtonDown()
    {
        _isBeingClicked = true;
        _image.color = Color.gray;
        onButtonDown?.Invoke();
    }

    public void ButtonUp()
    {
        _isBeingClicked = false;
        _image.color = _color;
        onButtonUp?.Invoke();
    }

    public void OnPointerUp(PointerEventData data)
    {
        ButtonUp();
    }
}
