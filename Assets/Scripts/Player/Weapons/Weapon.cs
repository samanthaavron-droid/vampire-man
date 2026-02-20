using UnityEngine;

public class Weapon
{
    public StatsTemplate statsTemplate;
    public Stats stats;
    public virtual void Use(PlayerBase _playerBase) { }
    public virtual void SpeedUpgrade() { }
    public virtual void DamageUpgrade() { }
    public virtual void RechargeUpgrade() { }
    public virtual void SizeUpgrade() { }
}
