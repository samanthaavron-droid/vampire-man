using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.EventSystems.EventTrigger;

public class StupidPathfinding : MonoBehaviour
{
    private GameObject player;

    private Vector2 _targetDirection;
    private Vector2 _checkedDirection;

    Vector3 lastDir = new Vector3();

    public float speed;
    void Start()
    {
        player = GameObject.Find("Player");
    }
    void Update()
    {
        FindPlayer();        
    }
    void FindPlayer()
    {
        //capturing direction towards a player
        _targetDirection = (player.transform.position - transform.position).normalized; 

        if (Mathf.Abs(_targetDirection.x) > Mathf.Abs(_targetDirection.y))
        {
            _checkedDirection = new Vector2(Mathf.Sign(_targetDirection.x), 0f);
        }
        else
        {
            _checkedDirection = new Vector2(0f, Mathf.Sign(_targetDirection.y));
        }

        if (CheckWallFront() == true)
        {
            //sending info to movement sys
       //     MovementSys.MoveTo(gameObject, _checkedDirection, speed, ref lastDir);
            //Debug.Log("Normal movement");
        } 
        else
        {
            if (Mathf.Abs(_targetDirection.x) > Mathf.Abs(_targetDirection.y))
            {
                _checkedDirection = new Vector2(0f, Mathf.Sign(_targetDirection.y));
            } 
            else
            {
                _checkedDirection = new Vector2(Mathf.Sign(_targetDirection.x), 0f);
            }
        //    MovementSys.MoveTo(gameObject, _checkedDirection, speed, ref lastDir);
            Debug.Log("wall movement");
        }
    }
    private bool CheckWallFront()
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, 0.3f, lastDir, 1f, mask);

        if (hit)
        {
            //Debug.Log("wall found");
            return false;
        } else
        {
            //Debug.Log("wall not found");
            return true; 
        }
    }
}
