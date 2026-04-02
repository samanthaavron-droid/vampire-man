using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : Seeker
{
    private UniversalBody _body;
    private ScoreManager _scoreManager;
    private HealthSys _healthSys;
    public bool randomWeapons;
    public bool followPlayer;

    public event Action<EnemyAI> OnDeath;

    private GameObject player;
    public Animator animator;
    private Vector2 currentDir;

    public Image healthBar;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _body = GetComponent<UniversalBody>();
        _scoreManager = GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>();
        _healthSys = GetComponent<HealthSys>();

        if (randomWeapons)
            StartingWeapon();

        AttackCheck();
        AnimationUpdate();
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
            if (Vector2.Distance(transform.position, player.transform.position) < 3f && PlayerBase.dead == false)
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

        HealthBarFollow();

        if (currentDir != _body.stats.currentDirection)
        {
            AnimationUpdate();
        }
    }
    private void HealthBarFollow()
    {
        healthBar.fillAmount = (_body.stats.health / _healthSys.startHealth);
    }
    private void AnimationUpdate()
    {
        currentDir = _body.stats.currentDirection;

        if (_body.stats.currentDirection.x > 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("right");
        else if (_body.stats.currentDirection.x < 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("left");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y > 0)
            animator.SetTrigger("up");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y < 0)
            animator.SetTrigger("down");

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
    public void DeathAnimationHealth()
    {
        if (_body.stats.currentDirection.x > 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("rightdeath");
        else if (_body.stats.currentDirection.x < 0 && _body.stats.currentDirection.y == 0)
            animator.SetTrigger("leftdeath");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y > 0)
            animator.SetTrigger("updeath");
        else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y < 0)
            animator.SetTrigger("downdeath");
    }
    public void StopPreDie()
    {
        _body.stats.movementSpeed = 0;
        healthBar.gameObject.SetActive(false);
    }
    public void Die() //Death system, can be called from outside
    {
        OnDeath?.Invoke(this);
        _scoreManager.AddXP(_body.stats.xp);
        Destroy(gameObject);
    }
}
