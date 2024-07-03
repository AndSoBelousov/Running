using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Input
{
    public class OldInputSystem : IInputSevice
    {
        public float HorizontalInput { get; private set; }

        public event Action SpacePressed;

        public void Update()
        {
            HorizontalInput = UnityEngine.Input.GetAxis("Horizontal");

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                SpacePressed?.Invoke();
            }   
        }
    }

}
