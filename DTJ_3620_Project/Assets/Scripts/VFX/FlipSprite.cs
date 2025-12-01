using UnityEngine;

public class FlipSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer = null;

    private void Start()
    {
        var _flip = Random.Range(0, 2) == 1;
        _renderer.flipX = _flip;
    }
}
