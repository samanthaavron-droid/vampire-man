using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float damage;
    public float health;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBase>().TakeDamage(damage);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(float damage)
    {
        health =- damage;
    }
}
