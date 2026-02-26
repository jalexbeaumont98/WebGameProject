using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonState : IModeState
{
    private ModeController controller;

    private InputAction switchToRolling;

    public ThirdPersonState(ModeController controller)
    {
        this.controller = controller;
    }

    public void Enter()
    {
        // Disable rolling movement
        // Enable third person movement
        // Enable shooter component
        // Configure camera: allowPitch = true
        // Possibly switch input action map to "TPS"

        controller.PlayerInput.SwitchCurrentActionMap("TPS");
        controller.SetModeTPS();

        switchToRolling = controller.PlayerInput.actions["S_Rolling"];

        // Subscribe
        switchToRolling.performed += OnSwitchToRolling;
        switchToRolling.Enable(); // usually already enabled by the action map, but safe

        Debug.Log("Entered Rolling Mode");
    }

    public void Exit()
    {
        // Cleanup if necessary
    }

    public void Tick()
    {
        // Handle TPS-specific logic
        // Example: if jump pressed and !grounded -> controller.SwitchToHelicopter();
    }

    public void FixedTick()
    {
        // TPS physics handled by TPS movement component
    }

    private void OnSwitchToRolling(InputAction.CallbackContext ctx)
    {
        controller.SwitchToRolling(); // ModeController handles Exit/Enter calls
    }
}
