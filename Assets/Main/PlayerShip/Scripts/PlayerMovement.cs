using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset _input;
    public float _screenWidth;

    private Rigidbody2D _rigidbody;
    private InputAction _moveAction;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_input is null)
        {
            Debug.LogError("input not assigned!");
        }

        _moveAction = _input.FindActionMap("gameplay").FindAction("move");
    }

    void FixedUpdate()
    {
        var inputX = _moveAction.ReadValue<float>() / Screen.width;
        var x = Mathf.Lerp(-_screenWidth, _screenWidth, inputX);

        var pos = _rigidbody.position;
        pos.Set(x, pos.y);
        _rigidbody.MovePosition(pos);
    }
}
