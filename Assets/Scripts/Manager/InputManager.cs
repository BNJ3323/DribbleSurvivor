using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Vector2 ReadMovementInput()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public InputField.InputType ReadTechnicalMoveInput()
    {
        return ReadTechnicalMoveInput();
    }

    public bool PauseInput()
    {
        return true;
    }
}
