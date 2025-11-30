using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _bar = null;
    [SerializeField] HealthBehaviour _health = null;

    private void LateUpdate()
    {
        _bar.fillAmount = _health.GetNormalizedValue();
    }
}
