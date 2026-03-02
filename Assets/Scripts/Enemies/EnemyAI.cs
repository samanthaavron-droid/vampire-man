using System.IO;
using UnityEngine;

public class EnemyAI : Seeker
{
    private UniversalBody _body => GetComponent<UniversalBody>();

    public float rayCastCheckDistance;
    void Start()
    {
        AttackCheck();
    }
    private void Update()
    {
        if (path == null || path.Count == 0 || target == null)
        {
            RequestPath();
            return;
        }

        float distance = Vector2.Distance(transform.position, path[targetIndex].Position);

        if (distance < 0.15f)
        {
            RequestPath();
        }
        if (path != null && path.Count > 0)
        {
            Vector3 dir = path[targetIndex].Position - (Vector2)transform.position;
            _body.stats.currentDirection = MovementSys.GetDirection(dir);
            transform.position += (Vector3)_body.stats.currentDirection * _body.stats.movementSpeed * Time.deltaTime;
        }
    }
    private void AttackCheck()
    {
        InvokeRepeating("Attack", 1f, 1f);
    }
    private void Attack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,
                                            _body.stats.currentDirection, 
                                            rayCastCheckDistance, 
                                            LayerMask.GetMask("Player"));
        if (hit)
        {
            _body.SecondaryAttack();
        }
    }
}
