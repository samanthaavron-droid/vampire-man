using System.Collections;
using UnityEngine;

public class TrapBody : MonoBehaviour
{
    Stats stats;
    Weapons weapons;
    public void SetStats(Stats stats, Weapons weapons)
    {
        this.stats = stats;
        this.weapons = weapons;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stats.tag))
        {
            if (collision.gameObject.GetComponent<UniversalBody>().spedUp == false)
            {
                 TrapAttack(collision.gameObject);

                if (gameObject.GetComponent<SpriteRenderer>() != null)
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
            } else
            {
                return;
            }
        }
    }
    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }
    private void TrapAttack(GameObject target)
    {
        target.GetComponent<HealthSys>().TakeDamage(stats.damage);

        target.gameObject.GetComponent<UniversalBody>().StartCoroutine(target.GetComponent<UniversalBody>().Stun(1000, stats.speed));

        if (target.gameObject == null)
        {
            Destroy(gameObject);
        }

        Destroy(gameObject);
    }
    private IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
