using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : HealthBehaviour
{
    [Header("// DEATH ANIMATION")]
    [SerializeField] SpriteRenderer _sprite = null;
    [SerializeField] float _duration = 1f;

    public static event UnityAction<HealthBehaviour> OnEnemyDie = null;

    protected override void OnDead_()
    {
        base.OnDead_();
        //gameObject.SetActive(false);
        //Destroy(gameObject);

        _sprite.color = Color.red;
        _sprite.DOFade(0, _duration).
            OnComplete(() => Destroy(gameObject));

        OnEnemyDie?.Invoke(this);
    }
}
