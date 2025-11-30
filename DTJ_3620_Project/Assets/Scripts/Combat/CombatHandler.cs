using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatHandler : Singleton<CombatHandler>
{
    [SerializeField] CanvasGroup _canvas = null;
    [SerializeField] PlayerData _playerData = default;
    [Space]
    [SerializeField] CanvasView _view = null;
    [SerializeField] DamageDisplay _damageUI = null;
    [SerializeField] StateController _state = null;
    [SerializeField] TextMeshProUGUI _enemyName = null;
    [Space]
    [SerializeField] AudioPlayer _hitEnemySfx = null;
    [SerializeField] AudioPlayer _hitPlayerSfx = null;
    [SerializeField] AudioPlayer _evadeSfx = null;
    [Space]
    [SerializeField] float _firstDuration = 2f;
    [SerializeField] float _secondDuration = 2f;
    [SerializeField] List<EnemyData> _enemies = null;

    [Header("// TIMER")]
    [SerializeField] Image _fill = null;
    [SerializeField] float _decisionTime = 7f;
    [SerializeField] float _timer = 0f;

    private EnemyData _enemyData = null;
    private EnemyHealth _enemyHealth = null;
    private PlayerHealth _playerHealth = null;

    public enum Turn
    {
        Player,
        Enemy
    }

    [Header("// DEBUG")]
    [SerializeField] Turn currentTurn = Turn.Player;

    public Turn CurrentTurn { get => currentTurn; }

    void Start()
    {
        _playerHealth = FindFirstObjectByType<PlayerHealth>();

        UpdateUI();
        //StartCombat();
    }

    private void Update()
    {
        if (!_view.IsVisible()) return;
        if (!_canvas.interactable) return;
        if (currentTurn != Turn.Player) return;

        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            StartEnemyTurn();
        }

        _fill.fillAmount = _timer / _decisionTime;
    }

    public void StartCombat()
    {
        var _index = Random.Range(0, _enemies.Count);
        _enemyData = _enemies[_index];
        _enemies.RemoveAt(_index);

        var _instance = Instantiate(_enemyData.Prefab, Vector3.zero, Quaternion.identity);
        _enemyHealth = _instance.GetComponent<EnemyHealth>();

        StartPlayerTurn();

        _enemyName.text = $"{_enemyData.DisplayName}";
        _view.Show();
    }

    void StartPlayerTurn()
    {
        _timer = _decisionTime;
        currentTurn = Turn.Player;
        UpdateUI();
    }

    public void PlayerAttack()
    {
        StartCoroutine(PlayerAttack_Routine());
    }

    IEnumerator PlayerAttack_Routine()
    {
        if (currentTurn != Turn.Player) yield break;

        _canvas.interactable = false;

        yield return new WaitForSeconds(_firstDuration);

        var _hasHit = Random.Range(0, 100) < _playerData.HitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            var _bonus = _playerData.AttackType == _enemyData.Weakness ? 3 : 1;
            _enemyHealth.TakeDamage(_damage * _bonus);
            _damageUI.Play($"{_damage * _bonus}", _bonus == 1 ? Color.white : Color.yellow);
            _hitEnemySfx.Play();

            if (!_enemyHealth.IsAlive())
            {
                // wait enemy death animation.
                yield return new WaitForSeconds(_secondDuration);
                _state.PlayWalkingState();
                _view.Hide();
                yield break;
            }
        }
        else
        {
            _damageUI.Play("Miss", Color.white);
            _evadeSfx.Play();
        }

        yield return new WaitForSeconds(_secondDuration);

        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        _timer = _decisionTime;
        currentTurn = Turn.Enemy;
        UpdateUI();
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        yield return new WaitForSeconds(_firstDuration);

        var _hasHit = Random.Range(0, 100) < _enemyData.HitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            _playerHealth.TakeDamage(_damage);
            _damageUI.Play($"{_damage}", Color.red);
            _hitPlayerSfx.Play();
        }
        else
        {
            _damageUI.Play("Miss", Color.red);
            _evadeSfx.Play();
        }

        if (!_playerHealth.IsAlive())
        {
            yield return new WaitForSeconds(_secondDuration);
            _view.Hide();
            yield return new WaitForSeconds(_secondDuration);
            EndgamePanel.Instance.Init(false);
            yield break;
        }

        yield return new WaitForSeconds(_secondDuration);

        StartPlayerTurn();
    }

    void UpdateUI()
    {
        // Player can only press the button during their own turn
        _canvas.interactable = (currentTurn == Turn.Player);
    }

    public bool HasDefeatedAllEnemies()
    {
        return _enemies.Count <= 0;
    }
}
