using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Vector2 _nextDirection;
    [HideInInspector] public Vector2 _currentDirection;
    private PlayerBase _playerBase; // Кешуємо компонент

    public InputActionReference moveDir;
    private float changeDirectionBuffer = 0.6f;
    private float curChangeDirectionBuffer;

    void Awake()
    {
        _playerBase = GetComponent<PlayerBase>();
    }

    void Update()
    {
        Vector2 input = moveDir.action.ReadValue<Vector2>();
        if (input.magnitude > 0.1f)
        {
            _nextDirection = MovementSys.GetDirection(input);
        }

        if (_nextDirection != _currentDirection)
        {
            if (MovementSys.CanMove(gameObject, _nextDirection))
            {
                _currentDirection = _nextDirection;
            }
        }

        if (MovementSys.CanMove(gameObject, _currentDirection))
        {
            MovementSys.Move(gameObject, _currentDirection, _playerBase.speed);
            MovementSys.ChangeRot(gameObject, _currentDirection);
        }
        else
        {
            MovementSys.SnapToAxis(gameObject, _currentDirection);
        }

        if (_nextDirection != _currentDirection)
        {
            if (curChangeDirectionBuffer > 0)
            {
                curChangeDirectionBuffer -= Time.deltaTime;
            }
            else
            {
                _nextDirection = _currentDirection;
                curChangeDirectionBuffer = changeDirectionBuffer;
            }
        }
    }
}