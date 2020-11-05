using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { Idle, Forward, Backward, Right, Left}

namespace Player.Controller 
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 _moveVector = Vector3.zero;
        [SerializeField] private float _speed = 0f;
        [SerializeField] private float _acelerate = 3f;
        [SerializeField] private float _deacelerate = 2f;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private Vector3 rotVector = new Vector3();
        [SerializeField] private float _wheelRotSpeed = 3f;
        [SerializeField] private List<Wheel> _wheels = new List<Wheel>();
        [SerializeField] private List<Wheel> _frontWheels = new List<Wheel>();

        private float _carRotateSpeed = 0.2f;

        public float Speed { get { return _speed; } set { _speed = value; } }
        public float Acelerate { get { return _acelerate; } set { _acelerate = value; } }
        public float MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
        public float Deacelerate { get { return _deacelerate; } set { _deacelerate = value; } }
        public List<Wheel> Wheels { get { return _wheels; } set { _wheels = value; } }
        public List<Wheel> FrontWheels { get { return _frontWheels; } }
        public float CarRotSpeed { get { return _carRotateSpeed; } set { _carRotateSpeed = value; } }

        //Cache
        private Vector2 _wheelRotateVector = new Vector2(); //to rotate wheel when moving left or right.
        private MoveDirection _prevDirection = MoveDirection.Idle; // Holds moving direction(forward/backward).

        /// <summary>
        /// Moves the Player car.
        /// </summary>
        public void Move(MoveDirection direction)
        {
            if (_speed <= 0) { return; }
            if (_speed >= _maxSpeed) { _speed = _maxSpeed; }
            _moveVector.z = _speed;

            if (direction == MoveDirection.Forward)
            {
                this.transform.localPosition += transform.TransformDirection(_moveVector * _acelerate * Time.deltaTime);
                _prevDirection = MoveDirection.Forward;
                MoveWheels(MoveDirection.Forward);
            }
            else if (direction == MoveDirection.Backward)
            {
                this.transform.position -= transform.TransformDirection(_moveVector * (_acelerate * 0.3f) * Time.deltaTime);
                _prevDirection = MoveDirection.Backward;
                MoveWheels(MoveDirection.Backward);
            }

            if ( direction == MoveDirection.Right)
            {
                if (_prevDirection == MoveDirection.Forward) { this.transform.Rotate(0f, _carRotateSpeed * Time.deltaTime, 0f); }
                else if (_prevDirection == MoveDirection.Backward) { this.transform.Rotate(0f, -_carRotateSpeed * Time.deltaTime, 0f); }
                MoveWheels(MoveDirection.Right);
            }
            else if (direction == MoveDirection.Left)
            {
                if (_prevDirection == MoveDirection.Forward) { this.transform.Rotate(0f, -_carRotateSpeed * Time.deltaTime, 0f); }
                else if (_prevDirection == MoveDirection.Backward) { this.transform.Rotate(0f, _carRotateSpeed * Time.deltaTime, 0f); }
                MoveWheels(MoveDirection.Left);
            }
        }

        public void MoveLeft()
        {
            foreach (Wheel wheel in _frontWheels)
            {
                _wheelRotateVector.y = -30f;
                wheel.transform.localEulerAngles = _wheelRotateVector;
            }
        }

        public void ResetWheels()
        {
            foreach (Wheel wheel in _frontWheels)
            {
                wheel.transform.localEulerAngles = Vector3.zero;
            }
        }

        public void MoveRight()
        {
            foreach (Wheel wheel in _frontWheels)
            {
                _wheelRotateVector.y = 30f;
                wheel.transform.localEulerAngles = _wheelRotateVector;
            }
        }

        /// <summary>
        /// Rotates Wheels of the vehicle.
        /// </summary>
        public void MoveWheels(MoveDirection direction)
        {
            foreach (Wheel wheel in _wheels)
            {
                switch (direction)
                {
                    case MoveDirection.Forward:
                        rotVector.x = _wheelRotSpeed;
                        wheel.Move(rotVector, _speed);
                        break;
                    case MoveDirection.Backward:
                        rotVector.x = -_wheelRotSpeed;
                        wheel.Move(rotVector, _speed);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}