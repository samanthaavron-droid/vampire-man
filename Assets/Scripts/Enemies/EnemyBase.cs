using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage;
    public int health;
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
            collision.gameObject.GetComponent<PlayerBase>().Damage(damage);
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Damage(int damage)
    {
        health =- damage;
    }
}
