using UnityEngine;
using UnityEngine.InputSystem;

public class RollingState : IModeState
{
    private ModeController controller;

    private InputAction switchToTPS;

    public RollingState(ModeController controller)
    {
        
        this.controller = controller;
    }

    public void Enter()
    {
        controller.PlayerInput.SwitchCurrentActionMap("Rolling");

        controller.SetModeRolling();

        switchToTPS = controller.PlayerInput.actions["S_TPS"];

        // Subscribe
        switchToTPS.performed += OnSwitchToTPS;
        switchToTPS.Enable(); // usually already enabled by the action map, but safe

        Debug.Log("Entered Rolling Mode");
    }

    public void Exit()
    {
        if (switchToTPS != null)
        {
            switchToTPS.performed -= OnSwitchToTPS;
            // No need to Disable() if you're switching maps, but it's okay either way:
            // switchToTPS.Disable();
        }
    }

    public void Tick()
    {
        // Handle rolling-specific input checks
        // Example: if player presses mode switch button -> controller.SwitchToThirdPerson();
    }

    public void FixedTick()
    {
        // Rolling physics handled by rolling movement component
    }

    private void OnSwitchToTPS(InputAction.CallbackContext ctx)
    {
        controller.SwitchToThirdPerson(); // ModeController handles Exit/Enter calls
    }
}
