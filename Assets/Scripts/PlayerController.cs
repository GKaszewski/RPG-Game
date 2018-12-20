using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator _anim;
    private CharacterController _characterController;
    private Transform _cameraTransform;
    private Vector3 _walkAcceleration;
    private float _jumpAcceleration;
    public float walkSpeed = 5f;
    public float walkHorizontalSpeed = 4f;
    public float walkBackwardSpeed = 3f;
    public float rotateSpeed = 100f;
    public float jumpStrength = 0.5f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = GameObject.FindWithTag("MainCamera").transform;

        if(GetComponent<Animator>() != null)
            _anim = GetComponent<Animator>();        
    }

    private void Update()
    {
        TakeKeyboardInput();
        DoMovement();
        CalculateAnimator();
    }

    private void TakeKeyboardInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            if(_characterController.isGrounded)
                _walkAcceleration += transform.forward * walkSpeed;
            else
                _walkAcceleration = transform.forward * walkSpeed;    
        }

        if(Input.GetKey(KeyCode.S))
        {
            if(_characterController.isGrounded)
                _walkAcceleration += transform.forward * -walkBackwardSpeed;
            else
                _walkAcceleration = transform.forward * -walkBackwardSpeed;    
        }

        if(Input.GetKey(KeyCode.A))
        {
            if(_characterController.isGrounded)
                _walkAcceleration += transform.right * -walkHorizontalSpeed;
            else
                _walkAcceleration = transform.right * -walkHorizontalSpeed;    
        }

        if(Input.GetKey(KeyCode.D))
        {
            if(_characterController.isGrounded)
                _walkAcceleration += transform.right * walkHorizontalSpeed;
            else
                _walkAcceleration = transform.right * walkHorizontalSpeed;    
        }

        if(Input.GetKey(KeyCode.Space))
        {
            _jumpAcceleration = jumpStrength;
        }
    }

    private void DoMovement()
    {
        _characterController.Move(_walkAcceleration * Time.deltaTime);
        _characterController.Move(new Vector3(0, _jumpAcceleration, 0));
        transform.eulerAngles = new Vector3(0, _cameraTransform.eulerAngles.y, 0);

        if (_characterController.isGrounded)
            _walkAcceleration = Vector3.zero;
        else
            _walkAcceleration = Vector3.MoveTowards(_walkAcceleration, Vector3.zero, Time.deltaTime);

        if(_jumpAcceleration > -0.98f)
            _jumpAcceleration = Mathf.MoveTowards(_jumpAcceleration, -0.98f, Time.deltaTime * 2f);    
    }
    
    private void CalculateAnimator()
    {
        
    }
}
