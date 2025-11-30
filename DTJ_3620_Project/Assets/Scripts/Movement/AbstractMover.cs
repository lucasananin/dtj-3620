using UnityEngine;

public abstract class AbstractMover : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rb = null;

    public virtual void SetVelocity(Vector3 _value)
    {
        _rb.linearVelocity = _value;
    }

    public virtual void InvertVelocity()
    {
        _rb.linearVelocity *= -1f;
    }

    public virtual void StopVelocity()
    {
        _rb.linearVelocity = Vector2.zero;
    }
}
