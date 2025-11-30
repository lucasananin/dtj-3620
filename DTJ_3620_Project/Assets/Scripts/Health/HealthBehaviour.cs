using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] protected bool _isInvincible = false;
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] UnityEvent OnHurt = null;
    [SerializeField] UnityEvent OnDie = null;
    //[SerializeField] protected int _defaultMaxHealth = 100;

    [Header("// READONLY")]
    [SerializeField] protected int _currentHealth = 0;

    public event UnityAction OnDamageTaken = null;
    public event UnityAction OnDead = null;
    public event UnityAction OnRestored = null;

    private void Awake()
    {
        RestoreAllHealth();
    }

    public void TakeDamage(int _value)
    {
        if (!IsAlive()) return;

        _currentHealth -= _value;

        if (_isInvincible)
            RestoreAllHealth();

        if (_currentHealth <= 0)
        {
            OnDead_();
        }
        else
        {
            OnDamageTaken_();
        }
    }

    public void RestoreAllHealth()
    {
        RestoreHealth(999);
    }

    public void RestoreHealth(int _percentage)
    {
        var _value = _maxHealth * (_percentage / 100f);
        _currentHealth += Mathf.RoundToInt(_value);
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        OnRestored?.Invoke();
    }

    protected virtual void OnDead_()
    {
        _currentHealth = 0;
        OnDead?.Invoke();
        OnDie?.Invoke();
    }

    protected virtual void OnDamageTaken_()
    {
        OnDamageTaken?.Invoke();
        OnHurt?.Invoke();
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public virtual float GetNormalizedValue()
    {
        return _currentHealth / (_maxHealth * 1f);
    }

    // UPGRADES
    public void IncreaseMaxHealth(int _percentage)
    {
        //var _buff = _defaultMaxHealth * (_percentage / 100f);
        //_maxHealth += (int)_buff;
        _maxHealth += _percentage;
    }

    //public void DecreaseMaxHealth(int _percentage)
    //{
    //    var _buff = _defaultMaxHealth * (_percentage / 100f);
    //    _maxHealth -= (int)_buff;
    //    _maxHealth = Mathf.Clamp(_maxHealth, 1, 9999);
    //}
}
