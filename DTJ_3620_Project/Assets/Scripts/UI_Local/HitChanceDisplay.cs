using TMPro;
using UnityEngine;

public class HitChanceDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tmp = null;
    [SerializeField] PlayerWeaponHandler _weaponHandler = null;
    [SerializeField] int _index = 0;

    private void LateUpdate()
    {
        _tmp.text = $"{_weaponHandler.GetHitChance(_index)}%";
    }
}
