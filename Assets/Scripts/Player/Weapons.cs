using UnityEngine;

public class Weapons : MonoBehaviour
{   
    public enum WeaponType
    {
        None,
        Dash
    }

    public WeaponType mainWeapon;
    public WeaponType secondaryWeapon;

    private MovementController _movementController => GetComponent<MovementController>();
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private LayerMask mask;

    Stats dashLevel = new Stats();
    void Start()
    {
        mask = LayerMask.GetMask("Enemy");
    }
    void Update()
    {
        
    }
    public void SetLevel()
    {

    }
    public void AttackMain()
    {
        switch (mainWeapon)
        {
            case WeaponType.None:
                
                break;

            case WeaponType.Dash:
                Dash();
                break;

            default:
                Debug.LogWarning("weapon type not recognized!");
                break;
        }
    }
    public void Dash()
    {
        if (Time.time < dashLevel.coolDown)
        {
            Debug.Log("dash on cooldown");
            return;
        }

        dashLevel.coolDown = Time.time + dashLevel.reChargeTime;

        Vector3 dashDir = _movementController.lastDir;

        Physics2D.IgnoreLayerCollision(0, 6, true);

        //transform position option
        transform.position += new Vector3(dashDir.x, dashDir.y, dashDir.z) * dashLevel.moveLength;

        RaycastHit2D dashHit = Physics2D.CircleCast(gameObject.transform.position, 0.5f, dashDir, 1f, mask);

        if (dashHit)
        {
            dashHit.collider.gameObject.GetComponent<EnemyBase>().Damage(dashLevel.damage);
        }

        Physics2D.IgnoreLayerCollision(0, 6, false);

        Debug.Log("Dash performed");
    }
}
public class Stats
{
    public float moveLength = 1f;
    public int damage = 1;
    public float reChargeTime = 1f;
    public float coolDown = 1f;
}
