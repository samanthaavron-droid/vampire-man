using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Objects/Stats")]
public class StatsTemplate : ScriptableObject, IStats
{
    [field:SerializeField] public float damage { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float reChargeTime { get; private set; }
    [field: SerializeField] public float size { get; private set; }
}
public interface IStats
{
    float damage { get; }
    float speed { get; }
    float reChargeTime { get; }
    float size { get; }
}
