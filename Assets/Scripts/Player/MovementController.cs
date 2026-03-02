using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Vector2 _nextDirection;
    private UniversalBody _body => GetComponent<UniversalBody>();

    public InputActionReference moveDir;
    private float changeDirectionBuffer = 0.6f;
    private float curChangeDirectionBuffer;
    void Update()
    {
        Vector2 input = moveDir.action.ReadValue<Vector2>();
        if (input.magnitude > 0.1f)
        {
            _nextDirection = MovementSys.GetDirection(input);
        }
        if (_nextDirection != _body.stats.currentDirection)
        {
            if (MovementSys.CanMove(gameObject, _nextDirection))
            {
                _body.stats.currentDirection = _nextDirection;
            }
        }

        if (MovementSys.CanMove(gameObject, _body.stats.currentDirection))
        {
                MovementSys.Move(gameObject, _body.stats.currentDirection, _body.stats.movementSpeed);
                MovementSys.ChangeRot(gameObject, _body.stats.currentDirection);
        }
        else
        {
            MovementSys.SnapToAxis(gameObject, _body.stats.currentDirection);
        }
        if(MovementSys.CanMove(gameObject, _body.stats.currentDirection) == false && _body.stats.currentDirection != _nextDirection)
        {
            MovementSys.SnapToAxis(gameObject, _body.stats.currentDirection);
        }

        if (_nextDirection != _body.stats.currentDirection)
        {
            if (curChangeDirectionBuffer > 0)
            {
                curChangeDirectionBuffer -= Time.deltaTime;
            }
            else
            {
                _nextDirection = _body.stats.currentDirection;
                curChangeDirectionBuffer = changeDirectionBuffer;
            }
        }
    }
}