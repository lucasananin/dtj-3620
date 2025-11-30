using UnityEngine;

public class EndgamePanel : Singleton<EndgamePanel>
{
    [SerializeField] CanvasView _victoryPanel = null;
    [SerializeField] CanvasView _defeatPanel = null;

    public void Init(bool _isVictory)
    {
        if (_isVictory)
        {
            _victoryPanel.Show();
            // play victory music
        }
        else
        {
            _defeatPanel.Show();
            // play defeat music.
        }
    }
}
