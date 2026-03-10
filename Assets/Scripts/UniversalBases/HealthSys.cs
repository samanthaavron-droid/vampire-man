using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSys : MonoBehaviour
{
    private UniversalBody _body;
    private ScoreManager _scoreManager;

    private float startHealth;
    [SerializeField] private float regenTime;
    private bool immune = false;
    private float coolDown;

    private void Start()
    {
        _body = GetComponent<UniversalBody>();
        startHealth = _body.stats.health;
        _scoreManager = GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>();
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
        //stolen screenshake
        if (_body.gameObject.tag == "Player")
        {
            if (damage >= startHealth / 5)
            {
                CameraShake.instance.Shake(CameraShake.Strength.strongShake);
            } else if (damage >= startHealth / 10)
            {
                CameraShake.instance.Shake(CameraShake.Strength.mediumShake);
            } else
            {
                CameraShake.instance.Shake(CameraShake.Strength.weakShake);
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
