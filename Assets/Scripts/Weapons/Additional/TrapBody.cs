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
                StartCoroutine(TrapAttack(collision.gameObject));
            } else
            {
                return;
            }
        }
    }
    private IEnumerator TrapAttack(GameObject target)
    {
        target.GetComponent<HealthSys>().TakeDamage(stats.damage);

        if (target.gameObject == null)
        {
            Destroy(gameObject);
        }

        float tempSpeed = target.GetComponent<UniversalBody>().stats.movementSpeed;

        if (tempSpeed != 0)
        {
            target.GetComponent<UniversalBody>().stats.movementSpeed = 0f;

            target.transform.position = gameObject.transform.localPosition;

            yield return new WaitForSeconds(stats.speed);

            if (target != null)
            {
                target.GetComponent<UniversalBody>().stats.movementSpeed = tempSpeed;
            }
        }
        Destroy(gameObject);
    }
}
