using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _yaw;
    private float _pitch;
    private Vector3 _rotationSmoothVelocity;
    private Vector3 _currentRotation;
    public Transform target;
    public bool isPlayerTarget = true;
    public float mouseSensitivity = 10f;
    public float distanceFromPlayer = 4f;
    public float rotationSmoothness = .12f;
    public Vector2 minMaxPitch;
    public Vector2 minMaxYaw;

    private void Awake()
    {
        if(isPlayerTarget)
            target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        _yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        _pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        _yaw = Mathf.Clamp(_yaw, minMaxYaw.x, minMaxYaw.y);
        _pitch = Mathf.Clamp(_pitch, minMaxPitch.x, minMaxPitch.y);

        _currentRotation = Vector3.SmoothDamp(_currentRotation, new Vector3(_pitch, _yaw), ref _rotationSmoothVelocity, rotationSmoothness);
        transform.eulerAngles = _currentRotation;
    }

    private void LateUpdate() => transform.position = target.position - transform.forward * distanceFromPlayer;
}
