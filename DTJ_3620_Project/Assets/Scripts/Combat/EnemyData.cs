using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] Vector2Int _damageRange = new(1, 2);
    [SerializeField] int _hitChance = 50;
    [SerializeField] DamageType _weakness = default;

    public GameObject Prefab { get => _prefab; }
    public Vector2Int DamageRange { get => _damageRange; }
    public int HitChance { get => _hitChance; }
    public DamageType Weakness { get => _weakness; }
}

public enum DamageType
{
    A,
    B,
    C,
}