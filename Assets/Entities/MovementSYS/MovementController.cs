using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Vector2 _movementDirection;
    private Vector2 _checkedMoveDirection;

    public float speed;

    public InputActionReference moveDir;
    void Start()
    {
        _movementDirection = new Vector2(0f, 1f);
    }
    void Update()
    {
        if (moveDir.action.ReadValue<Vector2>() != Vector2.zero)
        {
            _movementDirection = moveDir.action.ReadValue<Vector2>();
        }
        if (_movementDirection.x != 0)
        {
            _checkedMoveDirection = new Vector2(_movementDirection.x, 0f);
        } else
        {
            _checkedMoveDirection = new Vector2(0f, _movementDirection.y);
        }


            //sending info to movement system
            MovementSys.MoveTo(gameObject, _checkedMoveDirection, speed);
    }
}