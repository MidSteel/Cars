using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProceduralTerrainGeneratio.Map;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public delegate void OnUpdate();
    public static OnUpdate onUpdate;

    [SerializeField] private Cars _carsToInstantiate = null;

    public Cars CarsToInstantiate { get { return _carsToInstantiate; } }

    private List<ColorRegister> _colorRegList = new List<ColorRegister>();

    private void Awake()
    {
        if (instance == null) { instance = this; }

        foreach (CarData car in _carsToInstantiate.CarsData)
        {
            GameObject obj = Instantiate(car.car);
            _colorRegList.Add(obj.GetComponent<ColorRegister>());
            obj.name = car.car.name;
        }
    }


    private void Start()
    {
        ColorRegistersManager.instance.ColorRegisters = _colorRegList;
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        onUpdate?.Invoke();
    }
}
