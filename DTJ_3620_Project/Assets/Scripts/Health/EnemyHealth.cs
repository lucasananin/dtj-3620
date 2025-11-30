using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : HealthBehaviour
{
    public static event UnityAction<HealthBehaviour> OnEnemyDie = null;

    protected override void OnDead_()
    {
        base.OnDead_();
        //gameObject.SetActive(false);
        Destroy(gameObject);
        // fade and then destroy.
        OnEnemyDie?.Invoke(this);
    }
}
