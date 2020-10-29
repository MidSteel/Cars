using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRegisterHolder : MonoBehaviour
{
    private SetColor _setColor = null;
    private ColorRegister _colorRegister = null;
    private Renderer _renderer = null;

    public SetColor SetColor { get { return _setColor; } set { _setColor = value; } }
    public ColorRegister ColorRegister { get { return _colorRegister; } set { _colorRegister = value; } }

    private void Awake()
    {
        this.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SetSetColor);
    }

    public void SetRenderer()
    {
        _renderer = _setColor.GetComponent<Renderer>();
    }

    public void OnUpdate()
    {
        if (_renderer != null) { ColorManager.instance.SetColor(_renderer); }
    }

    public void SetSetColor()
    {
        _colorRegister.CurrentSetColor = _setColor;
        ColorManager.instance.SetRGB(_renderer.material.color);
        ColorManager.instance.CurrentColorRegisterHolder = this;
    }
}
