using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] PlayerData _data = null;
    [SerializeField] Vector2Int _increaseRange = new(8, 16);
    [SerializeField] Vector2Int _decreaseRange = new(8, 16);
    [SerializeField] WeaponData[] _weapons = null;

    public void Attack(int _index)
    {
        var _weapon = _weapons[_index];
        _data.AttackType = _weapon.Type;
        _data.HitChance = _weapon.HitChance;

        _weapon.HitChance -= Random.Range(_decreaseRange.x, _decreaseRange.y);
        _weapon.HitChance = Mathf.Clamp(_weapon.HitChance, 25, 100);
    }

    public void IncreaseHitChanceOthers(int _index)
    {
        int _count = _weapons.Length;

        for (int i = 0; i < _count; i++)
        {
            if (i == _index) continue;

            var _weapon = _weapons[i];
            _weapon.HitChance += Random.Range(_increaseRange.x, _increaseRange.y);
            _weapon.HitChance = Mathf.Clamp(_weapon.HitChance, 25, 100);
        }
    }

    public void IncreaseHitChanceAll()
    {
        int _count = _weapons.Length;

        for (int i = 0; i < _count; i++)
        {
            var _weapon = _weapons[i];
            _weapon.HitChance += Random.Range(_increaseRange.x, _increaseRange.y);
            _weapon.HitChance = Mathf.Clamp(_weapon.HitChance, 25, 100);
        }
    }
}

[System.Serializable]
public class WeaponData
{
    [SerializeField] DamageType _type = default;
    [SerializeField] int _hitChance = 0;

    public DamageType Type { get => _type; set => _type = value; }
    public int HitChance { get => _hitChance; set => _hitChance = value; }
}