using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : HealthBehaviour
{
    [SerializeField] UnityEvent _onPlayerDead = null;

    public static event UnityAction OnPlayerHurt = null;

    protected override void OnDamageTaken_()
    {
        base.OnDamageTaken_();
        OnPlayerHurt?.Invoke();
    }

    protected override void OnDead_()
    {
        base.OnDead_();
        gameObject.SetActive(false);

        _onPlayerDead?.Invoke();
    }

    public void UpdateMaxHealth(int _health)
    {
        _maxHealth += _health;

        if(_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
