using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 _movementDirection;
    private Vector2 _checkedMoveDirection;

    private PlayerBase _PlayerBase => GetComponent<PlayerBase>();

    Vector3 lastDir = new Vector3();

    public InputActionReference moveDir;
    void Start()
    {
        _movementDirection = new Vector2(0f, 0f);
    }
    void Update()
    {
        //only updating non 0 input
        if (moveDir.action.ReadValue<Vector2>() != Vector2.zero)
        {
            _movementDirection = moveDir.action.ReadValue<Vector2>();
        }
        //only sending a single direction
        if (_movementDirection.x != 0)
        {
            _checkedMoveDirection = new Vector2(_movementDirection.x, 0f);
        } else
        {
            _checkedMoveDirection = new Vector2(0f, _movementDirection.y);
        }

        //sending info to movement system
        MovementSys.MoveTo(gameObject, _checkedMoveDirection, _PlayerBase.speed, ref lastDir);
    }
}