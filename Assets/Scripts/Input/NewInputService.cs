using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runner.Input
{
    public class NewInputService : IInputSevice
    {
        private PlayerInput _input;

        public float HorizontalInput {  get; private set; }

        public event Action SpacePressed;

        public NewInputService()
        {
            _input = new PlayerInput();
            _input.Enable();

            _input.Move.Jumping.performed += OnSpacePressed;
        }


        public void Update()
        {
            HorizontalInput = _input.Move.Moving.ReadValue<float>();
        }


        private void OnSpacePressed(InputAction.CallbackContext context)
        {
            SpacePressed?.Invoke();
        }
    }

}
