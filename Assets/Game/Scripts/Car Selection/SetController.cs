using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetController : MonoBehaviour
{
    [SerializeField] private ButtonClick _forwardButton;
    [SerializeField] private ButtonClick _backwardButton;
    [SerializeField] private ButtonClick _rightButton;
    [SerializeField] private ButtonClick _leftButton;
    [SerializeField] private Transform _startPos; 

    public ButtonClick ForwardButton { get { return _forwardButton; } }
    public ButtonClick BackwardButton { get { return _backwardButton; } }
    public ButtonClick RightButton { get { return _rightButton; } }
    public ButtonClick LeftButton { get { return _leftButton; } }
    public Transform StartPos { get { return _startPos; } }

    private void Start()
    {
        GameObject.FindObjectOfType<PlayerTest>().SetController(this);
    }
}
