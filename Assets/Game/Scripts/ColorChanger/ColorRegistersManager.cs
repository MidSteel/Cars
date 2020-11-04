using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ColorRegistersManager : MonoBehaviour
{
    public static ColorRegistersManager instance = null;

    [SerializeField] private Button _nextButton = null;
    [SerializeField] private Button _prevButton = null;
    [SerializeField] private Transform _platformTransform = null;
    [SerializeField] private Transform _offPlatformTransform = null;

    private int _index = 0;
    private List<ColorRegister> _colorRegisters = new List<ColorRegister>();
    private ColorManager _colorManager = null;

    public List<ColorRegister> ColorRegisters { get { return _colorRegisters; } set { _colorRegisters = value; } }

    private void Awake()
    {
        instance = this;
        _nextButton.onClick.AddListener(Next);
        _prevButton.onClick.AddListener(Previous);
    }

    private void Start()
    {
        _colorManager = ColorManager.instance;
        StartCoroutine(Utility.DelayAction(1f, () => Next()));
    }

    public void Next()
    {
        _colorManager.CurrentColorRegisterHolder = null;
        _colorRegisters[_index].transform.position = _offPlatformTransform.position;
        _index = (_index >= _colorRegisters.Count - 1) ? 0 : _index + 1;
        _colorRegisters[_index].transform.position = _platformTransform.position;
        LoadColorButtons();
    }

    private void LoadColorButtons()
    {
        _colorManager.InstantiateButtons(_colorRegisters[_index]);
    }

    public void Previous()
    {
        _colorManager.CurrentColorRegisterHolder = null;
        _colorRegisters[_index].transform.position = _offPlatformTransform.transform.position;
        _index = (_index <= 0) ? _colorRegisters.Count - 1 : _index - 1;
        _colorRegisters[_index].transform.position = _platformTransform.position;
        LoadColorButtons();
    }
}
