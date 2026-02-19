using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [HideInInspector]
    public Vector2 movementDirection;

    public float speed;

    private PlayerBase _PlayerBase => GetComponent<PlayerBase>();

    [HideInInspector] public Vector3 lastDir = new Vector3();

    public InputActionReference moveDir;
    void Start()
    {
        movementDirection = new Vector2(0f, 0f);
    }
    void Update()
    {
        //only updating non 0 input
        if (moveDir.action.ReadValue<Vector2>() != Vector2.zero)
        {
            movementDirection = moveDir.action.ReadValue<Vector2>();
        }

        //sending info to movement system
        MovementSys.MoveTo(gameObject, movementDirection, speed, ref lastDir);
    }
}