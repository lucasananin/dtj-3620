using TMPro;
using UnityEngine;

public class TurnDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _tmp = null;
    [SerializeField] CombatHandler _combat = null;

    private void LateUpdate()
    {
        if (_combat.CurrentTurn == CombatHandler.Turn.Player)
        {
            _tmp.text = "Player Turn";
            _tmp.color = Color.cyan;
        }
        else
        {
            _tmp.text = "Enemy Turn";
            _tmp.color = Color.red;
        }
    }
}
