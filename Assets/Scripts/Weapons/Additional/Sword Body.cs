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
        }
    }
    private IEnumerator Stun(UniversalBody target)
    {
        if (target.stats.movementSpeed != 0)
        {
            targets.Add(target);

            float tempSpeed = target.stats.movementSpeed; //recording
            target.stats.movementSpeed = 0; //stunning

            yield return new WaitForSeconds(stats.speed);

            target.stats.movementSpeed = tempSpeed;
            targets.Remove(target);

            Deleter();
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
    private void LateUpdate()
    {
        transform.position = weapons.transform.position;
    }
}
