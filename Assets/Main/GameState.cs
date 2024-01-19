using UnityEngine;
using UnityEngine.InputSystem;

public class GameState : MonoBehaviour
{
    public InputActionAsset _input;

    private InputActionMap _gameplayInput;

    void Start()
    {
        _gameplayInput = _input.FindActionMap("gameplay");
        _gameplayInput.Enable();
    }

}
