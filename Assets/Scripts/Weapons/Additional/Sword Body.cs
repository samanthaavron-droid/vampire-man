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
    void Start()
    {
        StartCoroutine(RemoveAfterPlayerHit());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stats.tag))
        {
            collision.gameObject.GetComponent<HealthSys>().TakeDamage(stats.damage);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyAI ai = collision.gameObject.GetComponent<EnemyAI>();
                ai.StartStun(stats);
            }
        }
    }
    private IEnumerator RemoveAfterPlayerHit()
    { 
            yield return new WaitForSeconds(stats.speed);
            Destroy(gameObject);
        
    }
}
