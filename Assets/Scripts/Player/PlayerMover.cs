using Runner.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Runner.player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMover : MonoBehaviour
    {
        private const float GravityValue = -19f;
        [Header("Настройка характеристик персонажа")]
        [SerializeField] private InputType _inputType;
        [SerializeField, Range(0.2f, 3)] private float _forwardSpeed = 0.5f;
        [SerializeField, Range(1, 10)] private float _moveSpeed = 5;
        [SerializeField, Range(0.8f, 3)] private float _jumpPower = 1.2f;

        [Space(10)]
        [Header("Настройка усилителей")]
        [SerializeField, Range(1,2)] private float _tramplinePower = 1.5f;
        [SerializeField, Range(5, 15)] private float _boostSpeed = 10f; // Скорость при ускорении
        [SerializeField, Range(1, 5)] private float _boostDuration = 2f; // Длительность ускорения

        [Space(10)]
        [Header("Технические настройки местности")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 0.4f;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _tramplineMask;
        [SerializeField] private LayerMask _acceleratorMask;
            
        private CharacterController _characterController;
        private IInputSevice _input;
        private Vector3 _direction;
        private Vector3 _velocity;
        private float _currentSpeed;
        private bool _isGrounded;
        private bool _isTrampoline;
        private bool _isAccelerator;
        private bool _isBoosting = false;


        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _currentSpeed = _moveSpeed;

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

            MathOnTheGround();

            _direction = transform.right * _input.HorizontalInput + transform.forward * _forwardSpeed;
            _characterController.Move((_direction * _currentSpeed) * Time.deltaTime);

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
                _velocity.y = Mathf.Sqrt(_jumpPower *-2 * GravityValue);
            }

        }

        private void MathOnTheGround()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2;
                _velocity.z = 0;
                if (transform.position.z > 2.8f) //что бы сползать со стенок
                {
                    _velocity = transform.right * -2;
                }
                else if (transform.position.z < -2.7f)
                {
                    _velocity = transform.right * 2;
                }
            }
            else if (Physics.CheckSphere(_groundCheck.position, _groundDistance, _tramplineMask)) Trampoline(_tramplinePower);
            else if (Physics.CheckSphere(_groundCheck.position, _groundDistance, _acceleratorMask)) StartCoroutine(SpeedBoostCoroutine());


        }

        private void Trampoline(float tramplinePower)
        {
            float delta = Mathf.Sqrt(_jumpPower * -2 * GravityValue);
            _velocity.y = delta * tramplinePower;
        }

        private IEnumerator SpeedBoostCoroutine()
        {
            _isBoosting = true;
            _currentSpeed = _boostSpeed;

            yield return new WaitForSeconds(_boostDuration);

            _currentSpeed = _moveSpeed;
            _isBoosting = false;
        }
    }
}