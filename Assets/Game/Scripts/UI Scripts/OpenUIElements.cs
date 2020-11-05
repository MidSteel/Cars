using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenUIElements : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiElements = null;

    private Button _button = null;

    private void Awake()
    {
        _button = this.GetComponent<Button>();
        _button.onClick.AddListener(OpenElements);
    }

    private void OpenElements()
    {
        foreach (GameObject element in _uiElements)
        {
            element.SetActive(true);
        }
    }
}
