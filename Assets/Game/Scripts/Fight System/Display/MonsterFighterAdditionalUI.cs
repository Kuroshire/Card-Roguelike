using TMPro;
using UnityEngine;

public class MonsterFighterAdditionalUI : MonoBehaviour
{
    [SerializeField] MonsterFighter monster;
    [SerializeField] TextMeshProUGUI turnsBeforeAttack;

    void Start()
    {
        monster.OnTurnsLeftUpdate += SetTurnsLeftDisplay;

        SetTurnsLeftDisplay();
    }

    public void SetTurnsLeftDisplay() {
        turnsBeforeAttack.text = monster.TurnsLeftBeforeAttack.ToString();
    }
}
