using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public abstract class CameraShaker : MonoBehaviour
{
    [SerializeField] protected CinemachineImpulseSource _impulseSource = null;

    protected virtual void OnValidate()
    {
        if (_impulseSource == null)
            _impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void SetShape(CinemachineImpulseDefinition.ImpulseShapes _shape)
    {
        _impulseSource.ImpulseDefinition.ImpulseShape = _shape;
    }

    [ContextMenu("Shake()")]
    public abstract void Shake();
}
