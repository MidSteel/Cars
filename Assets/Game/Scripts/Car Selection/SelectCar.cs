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
        SceneManager.LoadScene(_sceneToLoad);
        _selctedCar.gameObject.AddComponent<PlayerTest>();
        _selctedCar.gameObject.AddComponent<Rigidbody>();
        _selctedCar.tag = "Player";
    }
}
