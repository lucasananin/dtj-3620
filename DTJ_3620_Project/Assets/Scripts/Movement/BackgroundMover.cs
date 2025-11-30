using UnityEngine;
using UnityEngine.Events;

public class BackgroundMover : AbstractMover
{
    [SerializeField] Transform _t = null;
    [SerializeField] Vector3 _direction = default;
    [SerializeField] float _speed = 5f;
    [Space]
    [SerializeField] float _restartPosition = -20f;
    [SerializeField] UnityEvent OnResetPositon_Event = null;

    [Header("// Readonly")]
    //[SerializeField] HealthBehaviour _playerHealth = null;
    [SerializeField] int _restartCount = 0;

    public event UnityAction OnResetPosition = null;

    public int RestartCount { get => _restartCount; private set => _restartCount = value; }

    //private void Awake()
    //{
    //    _playerHealth = FindFirstObjectByType<HealthBehaviour>();
    //}

    //private void Start()
    //{
    //    var _velocity = _direction * _speed;
    //    SetVelocity(_velocity);
    //}

    //private void OnEnable()
    //{
    //    _playerHealth.OnDeadAction += StopVelocity;
    //}

    //private void OnDisable()
    //{
    //    _playerHealth.OnDeadAction -= StopVelocity;
    //}

    private void FixedUpdate()
    {
        if (_t.position.z < _restartPosition)
        {
            _t.position = Vector3.zero;
            _restartCount++;
            OnResetPosition?.Invoke();
            OnResetPositon_Event?.Invoke();
            //Debug.Log($"{name} restarted!", this);
        }
    }

    public float GetTimeToComplete()
    {
        var _totalTime = _restartPosition / _speed;
        return Mathf.Abs(_totalTime);
    }

    public void Play()
    {
        var _velocity = _direction * _speed;
        SetVelocity(_velocity);
    }

    public void Stop()
    {
        StopVelocity();
    }
}
