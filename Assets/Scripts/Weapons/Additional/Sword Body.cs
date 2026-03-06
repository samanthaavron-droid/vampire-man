using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwordBody : MonoBehaviour
{
    Stats stats;
    Weapons weapons;
    List<UniversalBody> targets = new List<UniversalBody>();
    public void SetStats(Stats stats, Weapons weapons)
    {
        this.stats = stats;
        this.weapons = weapons;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stats.tag))
        {
            collision.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
            StartCoroutine(Stun(collision.gameObject.GetComponent<UniversalBody>()));
        } else
        {
            FallBack();
        }
    }
    private IEnumerator Stun(UniversalBody target)
    {
        if (target.stats.movementSpeed != 0 && target.gameObject.CompareTag("Enemy"))
        {
            targets.Add(target);

            float tempSpeed = target.stats.movementSpeed; //recording
            target.stats.movementSpeed = 0; //stunning
            //do something like stun VFX

            yield return new WaitForSeconds(stats.speed);

            target.stats.movementSpeed = tempSpeed;
            targets.Remove(target);

            Deleter();
        } else if (target.gameObject.CompareTag("Player"))
        {
            yield return new WaitForSeconds(stats.speed);
            Destroy(gameObject);
        }
    }
    private void Deleter()
    {
        if (targets.Count > 0)
        {
            return; 
        } else if (targets.Count == 0)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator FallBack()
    {
        yield return new WaitForSeconds(stats.speed);
        Destroy(gameObject);
    }
    private void Update()
    {
        transform.position = weapons.transform.position;
    }
}
