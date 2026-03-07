using NUnit.Framework;
using System.IO;
using UnityEngine;

public class EnemyAI : Seeker
{
    private UniversalBody _body => GetComponent<UniversalBody>();
    public bool randomWeapons;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (randomWeapons)
            StartingWeapon();

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

        //code that stops enemies from stopping 2 feet from the player
        if (Vector2.Distance(transform.position, player.transform.position) < 3f)
        {
            ignorePoints = true;
        }
        else
        {
            ignorePoints = false;
        }
    }
    private void AttackCheck()
    {
        InvokeRepeating("Attack", 1f, _body.weapons.mainWeapon.stats.reChargeTime);
    }
    private void Attack()
    {
        switch (_body.weapons.mWeapon)
        {
            case WeaponChoice.None:
                break;
            case WeaponChoice.Dash:
                RaycastHit2D hitDash = Physics2D.Raycast(transform.position,
                                                        _body.stats.currentDirection,
                                                        _body.stats.movementSpeed * _body.weapons.mainWeapon.stats.speed * 0.5f + 1f,
                                                        LayerMask.GetMask("Player"));
                if (hitDash)
                    _body.MainAttack();
                break;
            case WeaponChoice.Sword:
                Collider2D hitSword = Physics2D.OverlapCircle(transform.position,
                                                            _body.weapons.mainWeapon.stats.size,
                                                            LayerMask.GetMask("Player"));
                if (hitSword)
                    _body.MainAttack();
                break;
            case WeaponChoice.Whip:
                RaycastHit2D hitWhip = Physics2D.Raycast(transform.position,
                                                        _body.stats.currentDirection,
                                                        _body.weapons.mainWeapon.stats.size + 1f,
                                                        LayerMask.GetMask("Player"));
                if (hitWhip)
                    _body.MainAttack();
                break;
            case WeaponChoice.Trap:
                _body.MainAttack();
                break;
        }
    }
    private void StartingWeapon()
    {
        System.Array values = System.Enum.GetValues(typeof(WeaponChoice));
        _body.weapons.mWeapon = (WeaponChoice)values.GetValue(UnityEngine.Random.Range(0, 4)); //skipping Cross and None
        Debug.Log(_body.weapons.mWeapon);
        _body.weapons.SetWeapon();
    }
}
