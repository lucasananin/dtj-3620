using DG.Tweening;
using UnityEngine;

public class TweenShake : MonoBehaviour
{
    [SerializeField] Transform _t = null;
    [SerializeField] float _duration = 1f;
    [SerializeField] Vector3 _strength = default;
    [SerializeField] int _vibrato = 10;

    [ContextMenu("// Play()")]
    public void Play()
    {
        _t.DOShakePosition(_duration, _strength, _vibrato, 90);
    }
}
