using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyAI : Seeker
{
    private UniversalBody _body;
    private ScoreManager _scoreManager;
    public bool randomWeapons;
    public bool followPlayer;

    public event Action<EnemyAI> OnDeath;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _body = GetComponent<UniversalBody>();
        _scoreManager = GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>();

        if (randomWeapons)
            StartingWeapon();

        AttackCheck();
    }

    public void StartStun(Stats stat)
    {
        StartCoroutine(Stun(stat));
    }

    private IEnumerator Stun(Stats stat)
    {
        if (_body.stats.movementSpeed != 0 )
        {
            float tempSpeed = _body.stats.movementSpeed; //recording
            _body.stats.movementSpeed = 0; //stunning

            yield return new WaitForSeconds(stat.speed);

            _body.stats.movementSpeed = tempSpeed;
           
        }
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
        if (followPlayer == false)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 3f)
            {
                ignorePoints = true;
            }
            else
            {
                ignorePoints = false;
            }
        } else
        {
            ignorePoints = true;
        }
    }
    private void AttackCheck()
    {
        InvokeRepeating("Attack", 1f, 0.1f);
    }
    private void Attack()
    {
        switch (_body.weapons.mWeapon)
        {
            case WeaponChoice.None:
                break;
            case WeaponChoice.Dash:
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
    public void Die() //Death system, can be called from outside
    {
        OnDeath?.Invoke(this);
        Debug.Log(gameObject.name + " died");
        _scoreManager.AddXP(_body.stats.xp);
        Debug.Log("XP Added: " + _body.stats.xp);
        Destroy(gameObject);
    }
}
