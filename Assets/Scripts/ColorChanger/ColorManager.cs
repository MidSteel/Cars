using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance = null;
    
    public delegate void OnRegisterChange();
    public static OnRegisterChange onRegisterChange;

    [SerializeField] private int _buttonsPerRow = 5;
    [SerializeField] private Slider _r = null;
    [SerializeField] private Slider _g = null;
    [SerializeField] private Slider _b = null;
    [SerializeField] private GameObject _buttonsPrefab = null;
    [SerializeField] private RectTransform _instantiateParentObject = null;

    private ColorRegister _currentColorRegister = null;
    private List<ColorRegister> _setColorObjectList = new List<ColorRegister>();
    private Dictionary<ColorRegister, GameObject[]> _colorRegDictionary = new Dictionary<ColorRegister, GameObject[]>();

    public ColorRegisterHolder CurrentColorRegisterHolder { get { return _currentColorRegisterHolder; } set { _currentColorRegisterHolder = value; } }
    public ColorRegister CurrentColorRegister { get { return _currentColorRegister; } }

    //Cache
    private RectTransform _buttonObjectTransform = null;
    private ColorRegisterHolder _currentColorRegisterHolder = null;


    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }

    }

    private void Start()
    {
    }

    public void Update()
    {
        if (_currentColorRegisterHolder != null) { _currentColorRegisterHolder.OnUpdate(); }
    }

    /// <summary>
    /// Adds object to setColorObjectList;
    /// </summary>
    public void AddToColorObjectList(ColorRegister colorObject)
    {
        if (!_setColorObjectList.Contains(colorObject))
        {
            _setColorObjectList.Add(colorObject);
        }
    }

    /// <summary>
    /// Instantiates buttons.
    /// </summary>
    public void InstantiateButtons(ColorRegister colorRegister)
    {
        int xIndex = 0;
        float yPos = 0f;

        if (!_colorRegDictionary.ContainsKey(colorRegister))
        {

            _colorRegDictionary.Add(colorRegister, new GameObject[colorRegister.ColorObjects.Length]);

            for (int i = 0; i < colorRegister.ColorObjects.Length; i++)
            {
                GameObject obj = Instantiate(_buttonsPrefab, _instantiateParentObject);
                _buttonObjectTransform = obj.GetComponent<RectTransform>();
                Vector2 size = new Vector2(_instantiateParentObject.sizeDelta.x / _buttonsPerRow, _instantiateParentObject.sizeDelta.y / _buttonsPerRow);
                _buttonObjectTransform.sizeDelta = size;
                _buttonObjectTransform.GetComponentInChildren<RectTransform>().sizeDelta = size;
                if (i == _buttonsPerRow) { xIndex = 0; yPos -= _instantiateParentObject.sizeDelta.y / _buttonsPerRow; }
                _buttonObjectTransform.localPosition = new Vector3(xIndex * _buttonObjectTransform.sizeDelta.x, yPos);
                obj.GetComponentInChildren<Text>().text = colorRegister.ColorObjects[i].name;
                xIndex++;
                _colorRegDictionary[colorRegister][i] = obj;
                ColorRegisterHolder colorRegisterHolder;
                if (obj.TryGetComponent<ColorRegisterHolder>(out colorRegisterHolder)) { colorRegisterHolder.SetColor = colorRegister.ColorObjects[i]; colorRegisterHolder.ColorRegister = colorRegister; colorRegisterHolder.SetRenderer(); }
            }
        }

        DisableCurrentButtons(); 
        EnableButtons(colorRegister);
        onRegisterChange?.Invoke();
    }

    /// <summary>
    /// Disables buttons that dont belong to given register.
    /// </summary>
    private void DisableCurrentButtons()
    {
        if (_currentColorRegister == null) { return; }

        foreach (GameObject obj in _colorRegDictionary[_currentColorRegister])
        {
            obj.SetActive(false);
        }
    }

    private void EnableButtons(ColorRegister register)
    {
        if (!_colorRegDictionary.ContainsKey(register)) { return; }

        foreach (GameObject obj in _colorRegDictionary[register])
        {
            obj.SetActive(true);
        }

        _currentColorRegister = register;
    }

    public void SetRGB(Color color)
    {
        _r.value = color.r;
        _g.value = color.g;
        _b.value = color.b;
    }

    /// <summary>
    /// sets the color for the renderer.
    /// </summary>
    public void SetColor(Renderer renderer)
    {
        renderer.material.color = new Color(_r.value, _g.value, _b.value);
    }
}
