using Runner.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private InputType _inputType;

        private IInputSevice _input;

        private void Awake()
        {
            if(_inputType == InputType.Old)
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
            Debug.Log(_input.HorizontalInput);

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
            Debug.Log("Jump");
        }
    }

}
