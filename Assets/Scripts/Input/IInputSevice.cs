using System;



namespace Runner.Input
{
    public interface IInputSevice 
    {
        float HorizontalInput {  get; }

        event Action SpacePressed;

        void Update();
    }

}
