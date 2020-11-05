using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Attributes", menuName = "Game Data/Car Attributes")]
public class CarAttributes : ScriptableObject
{
    [SerializeField] private List<CarData> carData = new List<CarData>();

    public CarData GetCarData(GameObject car)
    {
        foreach (CarData data in carData)
        {
            if (data.car.name == car.name) { return data; }
            Debug.Log("Data : " + data.car.name);
            Debug.Log("Car : " + car.name);
        }

        return new CarData();
    }
}

[System.Serializable]
public struct CarData
{
    public GameObject car;
    public float weight;
    public float topSpeed;
    public float aceleration;
    public float deaceleration;
    public float carRotSpeed;
}
