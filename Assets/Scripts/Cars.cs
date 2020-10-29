using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cars", menuName = "Game Data/Cars")]
public class Cars : ScriptableObject
{
    [SerializeField] private GameObject[] _cars = null;

    public GameObject[] CarsData { get { return _cars; } }
}
