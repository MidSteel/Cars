using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    private ColorManager _colorManager = null;
    private Renderer _renderer = null;

    private void Start()
    {
        _colorManager = ColorManager.instance;
        _renderer = this.GetComponent<Renderer>();
    }

    public void OnChangeColor()
    {
        _colorManager.SetColor(_renderer);
    }
}
