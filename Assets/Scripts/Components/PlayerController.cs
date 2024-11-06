using UnityEngine;
using yiikes;

public class PlayerController : MonoBehaviour
{
    // Components
    MovementStateMachine movement;
    Kicker kicker;

    void Awake()
    {
        movement = GetComponent<MovementStateMachine>();
        kicker = GetComponent<Kicker>();
    }

    void Update()
    {
        // Movement inputs
        movement.MoveDirection = InputManager.instance.Movement;
        movement.JumpIsPressed = InputManager.instance.JumpIsPressed;
        movement.JumpWasPressed = InputManager.instance.JumpWasPressed;

        // Kicker inputs
        kicker.KickWasPressed = InputManager.instance.KickWasPressed;
        kicker.KickIsPressed = InputManager.instance.KickIsPressed;
        kicker.AimDirection = InputManager.instance.Movement;
    }
}
