using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCamOffset : MonoBehaviour
{
    [SerializeField] private Slider _xSlider;
    [SerializeField] private Slider _ySlider;
    [SerializeField] private Slider _zSlider;

    private CamFollow _camFollow = null;

    //Cache
    private Vector3 _camOffset;

    public void OnEnable()
    {
        _camFollow = GameObject.FindObjectOfType<CamFollow>();
        SetSliders();
        _xSlider.onValueChanged.AddListener(delegate { _camFollow.OnValueChange(); });
        _ySlider.onValueChanged.AddListener(delegate { _camFollow.OnValueChange(); });
        _zSlider.onValueChanged.AddListener(delegate { _camFollow.OnValueChange(); });
    }

    public void Update()
    {
        _camOffset.x = _xSlider.value;
        _camOffset.y = _ySlider.value;
        _camOffset.z = _zSlider.value;
        _camFollow.Offset = _camOffset;
    }

    private void SetSliders()
    {
        _xSlider.value = _camFollow.Offset.x;
        _ySlider.value = _camFollow.Offset.y;
        _zSlider.value = _camFollow.Offset.z;
    }
}
