using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class HealthSys : MonoBehaviour
{
    private UniversalBody _body;
    private ScoreManager _scoreManager;
    private CinemachineImpulseSource _impulseSource;

    private float startHealth;
    [SerializeField] private float regenTime;
    private bool immune = false;
    private float coolDown;

    private void Start()
    {
        _body = GetComponent<UniversalBody>();
        startHealth = _body.stats.health;
        _scoreManager = GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    private void Update()
    {
        if (coolDown <= 0) //Regeneration timer
        {
            RegenerationStart();
        } 
        else
        {
            coolDown -= Time.deltaTime;
        }
    }  
    public void TakeDamage(float damage) //Damage taking from outside
    {
        if (!immune)
        {
            _body.stats.health -= damage;
            coolDown = regenTime;
            Immunity(0.1f);
        }

        if(_body.stats.health <= 0)
        {
            if (_body.gameObject.tag == "Player")
            {
                Time.timeScale = 0;
                PlayerBase.dead = true;
            } else
            {
                gameObject.GetComponent<EnemyAI>().Die(); //animations of dying and everything else
            }
        }
        if (_body.gameObject.tag == "Player")
        {
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
    public void RegenerationStart() //regeneration called
    {
        if (_body.stats.health < startHealth)
        {
            InvokeRepeating("Regeneration", 1f, 1f);
        }
    }
    private void Regeneration() //regeneration
    {
        _body.stats.health += (startHealth / 10);
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
