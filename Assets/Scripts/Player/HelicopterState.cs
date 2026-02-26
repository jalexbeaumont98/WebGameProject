using UnityEngine;

public class HelicopterState : IModeState
{
    private ModeController controller;

    public HelicopterState(ModeController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        // Ensure player is not grounded
        // Reduce gravity effect
        // Increase air drag if needed
        // Enable limited air control
        // Shooter remains enabled
    }

    public void Exit()
    {
        // Restore normal gravity
        // Restore drag values
    }

    public void Tick()
    {
        // If grounded -> controller.SwitchToThirdPerson();
    }

    public void FixedTick()
    {
        // Apply helicopter-style physics (float, slow descent)
    }
}
