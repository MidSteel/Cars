using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private Vector3 _offset = Vector3.zero;
    [SerializeField] private GameObject _targetPointer = null;
    [SerializeField] private float _camFollowSpeed = 0.2f;
    [SerializeField] private float _camRotSpeed = 1f;

    private PostProcessVolume _postProcessVolume = null;
    private GameObject _target = null;
    private DepthOfField _depthOfField = null;

    public Vector3 Offset { get { return _offset; } set { _offset = value; } }

    //Cache
    private Vector3 _targetPointerEulerAngles = Vector3.zero;

    private void Start()
    {
        _postProcessVolume = this.GetComponent<PostProcessVolume>();
        _depthOfField = _postProcessVolume.profile.GetSetting<DepthOfField>();
        _target = GameObject.FindGameObjectWithTag("Player");

        if (_target != null) { this.transform.position = _target.transform.position + transform.TransformDirection(_offset); }
        //OnValueChange();
    }

    public void OnValueChange()
    {
        _depthOfField.focusDistance.value = Vector3.Distance(this.transform.position, _target.transform.position) / 2f;
    }

    private void LateUpdate()
    {
        if (_targetPointer != null)
        {
            if (_target == null) { return; }

            _targetPointerEulerAngles.x = _target.transform.localEulerAngles.x - _target.transform.localEulerAngles.x;
            _targetPointerEulerAngles.y = _target.transform.localEulerAngles.y;
            _targetPointerEulerAngles.z = _target.transform.localEulerAngles.z - _target.transform.localEulerAngles.z;

            _targetPointer.transform.position = _target.transform.position + Vector3.up;
            _targetPointer.transform.localEulerAngles = _targetPointerEulerAngles;

            this.transform.localRotation = _targetPointer.transform.localRotation;
            this.transform.localPosition = Vector3.Lerp(this.transform.position, _targetPointer.transform.position + transform.TransformDirection(_offset), _camFollowSpeed * Time.deltaTime);
            this.transform.LookAt(Vector3.Lerp(this.transform.position, _target.transform.position, _camRotSpeed));

            OnValueChange();
        }
    }
}
