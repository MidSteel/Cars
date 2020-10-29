using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 3f;

    private ColorRegister _colorReg = null;
    private Vector2 _carRotVector = new Vector2();
    private ColorManager _colorManager = null;

    private void Start()
    {
        _colorManager = ColorManager.instance;
        ColorManager.onRegisterChange += ChangeRegister;
    }

    private void Update()
    {
        if (_colorReg != null)
        {
            _carRotVector.y += _rotSpeed;
            _colorReg.transform.Rotate(_carRotVector * Time.deltaTime);
            _carRotVector = Vector2.zero;
        }
    }

    public void ChangeRegister()
    {
        if (_colorManager == null) { _colorManager = ColorManager.instance; }

        _colorReg = _colorManager.CurrentColorRegister;
    }
}
