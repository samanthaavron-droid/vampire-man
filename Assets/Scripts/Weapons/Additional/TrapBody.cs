using System.Collections;
using UnityEngine;

public class TrapBody : MonoBehaviour
{
    Stats stats;
    Weapons weapons;

    private bool trapped = false;
    public void SetStats(Stats stats, Weapons weapons)
    {
        this.stats = stats;
        this.weapons = weapons;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stats.tag) && trapped == false)
        {
            trapped = true;

            StartCoroutine(TrapAttack(collision.gameObject));            
        }
    }
    private IEnumerator TrapAttack(GameObject target)
    {
        target.GetComponent<HealthSys>().TakeDamage(stats.damage);

        float tempSpeed = target.GetComponent<UniversalBody>().stats.movementSpeed;
        target.GetComponent<UniversalBody>().stats.movementSpeed = 0f;

        target.transform.position = gameObject.transform.localPosition;

        yield return new WaitForSeconds(stats.speed);

        target.GetComponent<UniversalBody>().stats.movementSpeed = tempSpeed;

        Destroy(gameObject);
    }
}
