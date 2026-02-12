using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private GameObject _player;

    public int damage;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
