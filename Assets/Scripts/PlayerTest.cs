﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Player.Controller;

[RequireComponent(typeof(PlayerController))]
public class PlayerTest : MonoBehaviour
{
    [SerializeField] private ButtonClick _forwardButton = null;
    [SerializeField] private ButtonClick _backwardButton = null;
    [SerializeField] private ButtonClick _rightButton = null;
    [SerializeField] private ButtonClick _leftButton = null;

    private bool _deAcelerate = false;
    private PlayerController _playerController = null;
    private MoveDirection _previousDirection = MoveDirection.Idle;
    
    public ButtonClick RightButton { get { return _rightButton; } }
    public ButtonClick LeftButton { get { return _leftButton; } }


    private void Awake()
    {
        _playerController = this.GetComponent<PlayerController>();
    }

    public void SetController(SetController controllerInfo)
    {
        _forwardButton = controllerInfo.ForwardButton;
        _backwardButton = controllerInfo.BackwardButton;
        _rightButton = controllerInfo.RightButton;
        _leftButton = controllerInfo.LeftButton;
        this.transform.position = controllerInfo.StartPos.position;
        
        Wheel[] wheels = this.GetComponentsInChildren<Wheel>();
        _playerController.Wheels = wheels.ToList();
        foreach (Wheel wheel in wheels)
        {
            if (wheel.IsFront) { _playerController.FrontWheels.Add(wheel); }
        }

        _leftButton.onButtonDown += _playerController.MoveLeft;
        _rightButton.onButtonDown += _playerController.MoveRight;
        _leftButton.onButtonUp += _playerController.ResetWheels;
        _rightButton.onButtonUp += _playerController.ResetWheels;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A)) { MoveLeft(); }
        else if (Input.GetKey(KeyCode.D)) { MoveRight(); }

        if (_forwardButton.IsClicked) { MoveForward(); }
        else if (_backwardButton.IsClicked) { MoveBackward(); }

        if (_rightButton.IsClicked) { MoveRight(); }
        else if (_leftButton.IsClicked) { MoveLeft(); }

        if (ShouldDeacelerate()) { Deacelerate(0.05f); }
    }

    private void Deacelerate(float deacelerationspeed)
    {
        _playerController.Speed = (_playerController.Speed <= deacelerationspeed) ? 0f : _playerController.Speed -= (_playerController.Deacelerate * Time.deltaTime); _playerController.Move(_previousDirection);
    }

    private bool ShouldDeacelerate()
    {
        if (!_forwardButton.IsClicked && !_backwardButton.IsClicked) { return true;  }

        return false;
    }

    private void MoveForward() 
    {
        if (_previousDirection == MoveDirection.Backward) { _playerController.Speed = 0f; }

        _previousDirection = MoveDirection.Forward;
        _playerController.Move(MoveDirection.Forward);
        _playerController.Speed += Time.deltaTime;
        _previousDirection = MoveDirection.Forward;
    }

    private void MoveBackward()
    {
        if (_previousDirection == MoveDirection.Forward) { _playerController.Speed = 0f; }
        _previousDirection = MoveDirection.Backward;
        _playerController.Move(MoveDirection.Backward);
        _playerController.Speed += Time.deltaTime;
    }

    private void MoveRight()
    {
        if (_playerController.Speed <= 0f) { return; }
        _playerController.Move(MoveDirection.Right);
    }

    private void MoveLeft()
    {
        if (_playerController.Speed <= 0f) { return; }
        _playerController.Move(MoveDirection.Left);
    }
}
