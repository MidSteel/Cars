using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCar : MonoBehaviour
{
    [SerializeField] private int _sceneToLoad;

    private Button _button;
    private ColorRegister _selctedCar;

    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(CarSelect);
        DontDestroyOnLoad(this);
    }

    public void CarSelect()
    {
        _selctedCar = ColorManager.instance.CurrentColorRegister;
        DontDestroyOnLoad(_selctedCar);
        GameObject.FindObjectOfType<PreloadData>().LoadScene(_sceneToLoad);
        _selctedCar.gameObject.AddComponent<PlayerTest>();
        _selctedCar.gameObject.AddComponent<Rigidbody>();
        CarData carData = GameManager.instance.CarsToInstantiate.GetData(_selctedCar.name);
        SetCarData(carData);
        _selctedCar.transform.localEulerAngles = Vector3.zero;
        _selctedCar.tag = "Player";
    }

    private void SetCarData(CarData data)
    {
        if (_selctedCar == null) { return; }

        _selctedCar.GetComponent<Rigidbody>().mass = data.weight;
        Player.Controller.PlayerController controller = _selctedCar.GetComponent<Player.Controller.PlayerController>();
        controller.Acelerate = data.aceleration;
        controller.Deacelerate = data.deaceleration;
        controller.MaxSpeed = data.topSpeed;
        controller.CarRotSpeed = data.carRotSpeed;
    }
}
