using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int damage;
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
}
