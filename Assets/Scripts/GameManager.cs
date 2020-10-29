using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Cars _carsToInstantiate;

    private List<ColorRegister> _colorRegList = new List<ColorRegister>();

    private void Awake()
    {
        foreach (GameObject car in _carsToInstantiate.CarsData)
        {
            GameObject obj = Instantiate(car);
            _colorRegList.Add(obj.GetComponent<ColorRegister>());
        }
    }

    private void Start()
    {
        ColorRegistersManager.instance.ColorRegisters = _colorRegList;
    }
}
