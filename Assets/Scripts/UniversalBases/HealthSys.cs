using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HealthSys : MonoBehaviour
{
    private UniversalBody _body;
    private Animator animator;
    public CinemachineImpulseSource _impulseSource;

    [HideInInspector]public float startHealth;
    [SerializeField] private float regenTime;
    private bool immune = false;
    private float coolDown;
    private bool regeneration = false;

    public ParticleSystem damageEffect;
    private void Start()
    {
        _body = GetComponent<UniversalBody>();
        startHealth = _body.stats.health;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            return;
        }
        else if (coolDown < 0 && _body.stats.health < startHealth)
        {
            StartCoroutine(RegenerationStart());
        }
    }  
    public void TakeDamage(float damage) //Damage taking from outside
    {
        if (!immune)
        {
            _body.stats.health -= damage;
            damageEffect.Play();
            //Debug.Log("Health left: " + _body.stats.health);
            coolDown = regenTime;
            Immunity(0.1f);
        }

        if(_body.stats.health <= 0)
        {
            if (_body.gameObject.tag == "Player")
            {
                PlayerBase.dead = true;
            } else
            {
                gameObject.GetComponent<EnemyAI>().DeathAnimationHealth(); //animations of dying and everything else
                gameObject.GetComponent<EnemyAI>().StopPreDie();
                StartCoroutine(DeathController(gameObject.GetComponent<EnemyAI>()));
            }
        }
        if (_body.gameObject.tag == "Player")
        {
            if (_body.stats.currentDirection.x > 0 && _body.stats.currentDirection.y == 0)
                animator.SetTrigger("rightdamage");
            else if (_body.stats.currentDirection.x < 0 && _body.stats.currentDirection.y == 0)
                animator.SetTrigger("leftdamage");
            else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y > 0)
                animator.SetTrigger("updamage");
            else if (_body.stats.currentDirection.x == 0 && _body.stats.currentDirection.y < 0)
                animator.SetTrigger("downdamage");

            if (damage >= _body.stats.health / 2)
            {
                _impulseSource.GenerateImpulse(1f);
            } else if (damage >= _body.stats.health / 5)
            {
                _impulseSource.GenerateImpulse(0.5f);
            } else
            {
                _impulseSource.GenerateImpulse(0.2f);
            }
        }
    }
    private IEnumerator DeathController(EnemyAI ai)
    {
        yield return new WaitForSeconds(0.5f);
        ai.Die();
    }
    public IEnumerator RegenerationStart() //regeneration called
    {
        Regeneration();

        if (regeneration == true)
        {
            yield return new WaitForSecondsRealtime(1f);
            regeneration = true;
            StartCoroutine(RegenerationStart());
        } else
        {
            yield return null;
        }
    }
    private void Regeneration() //regeneration
    {
        _body.stats.health += (startHealth / 10);

        if (_body.gameObject.tag == "Player")
        {
            ScoreManager.levelXP -= Mathf.RoundToInt(startHealth / 10);
        }

        if (_body.stats.health > startHealth)
        {
            _body.stats.health = startHealth;
        }

        if (_body.stats.health == startHealth)
        {
            regeneration = false;
        }        
    }
    public void Immunity(float time) //Immunity system and it's timer
    {
        immune = true;
        StartCoroutine(ImmunityTimer(time));
    }
    private IEnumerator ImmunityTimer(float time) //said timer
    {
        yield return new WaitForSeconds(time);
        immune = false;
    }
}
