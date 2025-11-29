using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] DamageType _attackType = default;
    [SerializeField] int _hitChance = 0;

    public DamageType AttackType { get => _attackType; set => _attackType = value; }
    public int HitChance { get => _hitChance; set => _hitChance = value; }
}
