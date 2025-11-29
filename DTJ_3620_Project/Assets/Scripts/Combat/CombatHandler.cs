using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatHandler : MonoBehaviour
{
    public Button endTurnButton;
    public float _firstDuration = 2f;
    public float _secondDuration = 2f;
    [SerializeField] EnemyData[] _enemies = null;

    private EnemyData _enemyData = null;
    private EnemyHealth _enemyHealth = null;
    private PlayerHealth _playerHealth = null;
    private DamageType _playerDamageType = default;

    public enum Turn
    {
        Player,
        Enemy
    }

    public Turn currentTurn = Turn.Player;

    void Start()
    {
        _playerHealth = FindFirstObjectByType<PlayerHealth>();
        endTurnButton.onClick.AddListener(PlayerAttack);

        UpdateUI();
        StartCombat();
    }

    public void StartCombat()
    {
        // show combat ui.

        var _index = Random.Range(0, _enemies.Length);
        _enemyData = _enemies[_index];

        var _instance = Instantiate(_enemyData.Prefab, Vector3.zero, Quaternion.identity);
        _enemyHealth = _instance.GetComponent<EnemyHealth>();

        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        currentTurn = Turn.Player;
        UpdateUI();
        Debug.Log("Player turn started.");
    }

    void PlayerAttack()
    {
        StartCoroutine(PlayerAttack_Routine());
    }

    IEnumerator PlayerAttack_Routine()
    {
        if (currentTurn != Turn.Player) yield break;

        endTurnButton.interactable = false;

        yield return new WaitForSeconds(_firstDuration);

        Debug.Log("Player attacked.");
        var _weaponHitChance = 90;
        var _hasHit = Random.Range(0, 100) < _weaponHitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            var _bonus = _playerDamageType == _enemyData.Weakness ? 3 : 1;
            _enemyHealth.TakeDamage(_damage * _bonus);
            Debug.Log("Player Hit");

            if (!_enemyHealth.IsAlive())
            {
                // destroy enemy prefab.
                // wait enemy death animation.
                // hide combat ui.
                // start walking state.
                yield break;
            }
        }
        else
        {
            Debug.Log("Player Missed");
        }

        yield return new WaitForSeconds(_secondDuration);

        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        currentTurn = Turn.Enemy;
        UpdateUI();
        StartCoroutine(EnemyTurnRoutine());
    }

    private IEnumerator EnemyTurnRoutine()
    {
        Debug.Log("Enemy turn started.");

        yield return new WaitForSeconds(_firstDuration);

        Debug.Log("Enemy attacked.");
        var _hasHit = Random.Range(0, 100) < _enemyData.HitChance;

        if (_hasHit)
        {
            var _damage = Random.Range(_enemyData.DamageRange.x, _enemyData.DamageRange.y);
            _playerHealth.TakeDamage(_damage);
            Debug.Log("Enemy Hit");
        }
        else
        {
            Debug.Log("Enemy Missed");
        }

        if (!_playerHealth.IsAlive())
        {
            // endgame.
            yield break;
        }

        yield return new WaitForSeconds(_secondDuration);

        Debug.Log("Enemy finished their turn.");
        StartPlayerTurn();
    }

    void UpdateUI()
    {
        // Player can only press the button during their own turn
        endTurnButton.interactable = (currentTurn == Turn.Player);
    }


}
