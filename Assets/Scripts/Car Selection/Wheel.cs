using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Controller;

public class Wheel : MonoBehaviour
{
    [SerializeField] private bool _isFront = false;

    public bool IsFront { get { return _isFront; } }

    private void OnEnable()
    {
    }

    public void SetPlayerWheels()
    {

    }

    public void Move(Vector3 moveVector, float speed)
    {
        this.transform.Rotate(moveVector * speed);
    }
}
