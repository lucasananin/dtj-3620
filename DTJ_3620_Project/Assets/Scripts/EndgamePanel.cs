using System.Collections;
using UnityEngine;

public class EndgamePanel : Singleton<EndgamePanel>
{
    [SerializeField] CanvasView _flash = null;
    [SerializeField] CanvasView _victoryPanel = null;
    [SerializeField] CanvasView _defeatPanel = null;

    public void Init(bool _isVictory)
    {
        if (_isVictory)
        {
            StartCoroutine(Victory_Routine());
        }
        else
        {
            _defeatPanel.Show();
            // play defeat music.
        }
    }

    IEnumerator Victory_Routine()
    {
        _flash.Show();
        yield return new WaitForSeconds(2);
        _victoryPanel.Show();
        // play victory music
    }
}
