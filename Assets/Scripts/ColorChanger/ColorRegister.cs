using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRegister : MonoBehaviour
{
    private SetColor[] _colorObjects = null;
    private ColorManager _colorManager = null;
    private SetColor _currentSetColor = null;

    public SetColor[] ColorObjects { get { return _colorObjects; } }
    public SetColor CurrentSetColor { get { return _currentSetColor; } set { _currentSetColor = value; } }

    private void Awake()
    {
        _colorObjects = this.GetComponentsInChildren<SetColor>();
    }

    private void Start()
    {
        ColorManager.instance.AddToColorObjectList(this);
    }
}
