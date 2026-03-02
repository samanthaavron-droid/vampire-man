using System.Collections;
using UnityEngine;

public class HealthSys : MonoBehaviour
{
    private UniversalBody _body => GetComponent<UniversalBody>();

    private float startHealth;
    [SerializeField] private float regenTime;
    private bool immune = false;
    private float coolDown;

    private void Start()
    {
        startHealth = _body.stats.health;
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
            Die();
            //animations of dying and everything else
        }
    }
    public void Die() //Death system, can be called from outside
    {
        Destroy(gameObject);
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
