using System.Collections;
using UnityEngine;

public class TrapBody : MonoBehaviour
{
    Stats stats;

    private float tempSpeed;
    private GameObject target;
    private bool trapped = false;
    public void SetStats(Stats stats)
    {
        this.stats = stats;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(stats.tag) && trapped == false)
        {
            trapped = true;

            target = collision.gameObject;

            StartCoroutine(TrapAttack());            
        }
    }
    private IEnumerator TrapAttack()
    {
        target.GetComponent<HealthSys>().TakeDamage(stats.damage);

        tempSpeed = target.GetComponent<UniversalBody>().stats.movementSpeed;
        target.GetComponent<UniversalBody>().stats.movementSpeed = 0f;

        target.transform.position = gameObject.transform.localPosition;

        yield return new WaitForSeconds(stats.speed);

        target.GetComponent<UniversalBody>().stats.movementSpeed = tempSpeed;

        Destroy(gameObject);
    }
}
