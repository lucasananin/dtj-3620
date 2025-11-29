using DG.Tweening;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tmp = null;
    [SerializeField] Vector3 _offset = default;
    [SerializeField] float _jumpPower = 1f;
    [SerializeField] float _duration= 2f;

    private Vector3 _defaultPosition = default;

    private void Awake()
    {
        _defaultPosition = _tmp.transform.position;
        _tmp.color = new Color(_tmp.color.r, _tmp.color.g, _tmp.color.b, 0);
    }

    public void Play(string _text, Color _color)
    {
        _tmp.text = $"{_text}";
        _tmp.color = _color;

        _tmp.color = new Color(_tmp.color.r, _tmp.color.g, _tmp.color.b, 1);
        _tmp.transform.position = _defaultPosition;
        _tmp.transform.DOJump(_tmp.transform.position + _offset, _jumpPower, 1, _duration);
        _tmp.DOFade(0, _duration);
    }

    [ContextMenu("Fodase")]
    private void Fodase()
    {
        _tmp.color = new Color(_tmp.color.r, _tmp.color.g, _tmp.color.b, 1);
        _tmp.transform.position = _defaultPosition;
        _tmp.transform.DOJump(_tmp.transform.position + _offset, _jumpPower, 1, _duration);
        _tmp.DOFade(0, _duration);
    }
}
