using Runner.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private const float GravityValue = -19f;
        [SerializeField] private InputType _inputType;
        [SerializeField] private float _forwardSpeed = 0.5f;
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _jumpHeight = 4;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;

        private CharacterController _characterController;

        private IInputSevice _input;
        private Vector3 _direction;
        private Vector3 _velocity;
        private bool _isGrounded;
 

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();

            if (_inputType == InputType.Old)
            {
                _input = new OldInputSystem();
            }
            else
            {
                _input = new NewInputService();
            }
        }

        private void Update()
        {
            _input.Update();

            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
                _velocity.z = 0;
                if (transform.position.z > 2.8f) //что бы сползать со стенок
                {
                    _velocity= transform.right * -2;
                }
                else if(transform.position.z < -2.7f) 
                {
                    _velocity = transform.right * 2; 
                }
            }
            _direction = transform.right * _input.HorizontalInput + transform.forward * _forwardSpeed;
   
            
            _characterController.Move((_direction * _moveSpeed) * Time.deltaTime);

            _velocity.y += GravityValue * Time.deltaTime;
            
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private void OnEnable()
        {
            _input.SpacePressed += OnSpacePressed;
        }

        private void OnDisable()
        {
            _input.SpacePressed -= OnSpacePressed;
        }

        private void OnSpacePressed()
        {
            //Debug.Log(_characterController.isGrounded);
            if (_isGrounded)
            {
                _velocity.y = Mathf.Sqrt(_jumpHeight *-2 * GravityValue);
            }

        }

        
    }
}