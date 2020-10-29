using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = Vector3.zero;
    
    private GameObject _target = null;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            this.transform.position = _target.transform.position + _offset;
            this.transform.LookAt(_target.transform);
        }
    }
}
