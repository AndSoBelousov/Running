using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Runner.Input
{
    public interface IInputSevice 
    {
        float HorizontalInput {  get; }

        event Action SpacePressed;

        void Update();
    }

}
