using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cars", menuName = "Game Data/Cars")]
public class Cars : ScriptableObject
{
    [SerializeField] private CarData[] _cars = null;

    public CarData[] CarsData { get { return _cars; } }

    public CarData GetData(string name)
    {
        foreach (CarData data in _cars)
        {
            if (data.car.name == name) { return data; }
        }

        return new CarData();
    }
}
