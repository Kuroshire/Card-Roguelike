using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndOfFightScreen : MonoBehaviour
{
    [SerializeField] private GameObject endOfFightScreen;
    [SerializeField] private TextMeshProUGUI winnerText;

    void Start()
    {
        FightSystemManager.TurnBasedFight.OnFightOver += ActivateEndOfFight;
        TurnOff();
    }

    public void ActivateEndOfFight() {
        FighterTeam winners = FightSystemManager.TurnBasedFight.WinningTeam;

        switch(winners) {
            case FighterTeam.Players:
                winnerText.text = "Players Won !";
                break;
            case FighterTeam.Monsters:
                winnerText.text = "Monsters Won !";
                break;
            case FighterTeam.None:
                winnerText.text = "Draw !";
                break;
        }

        TurnOn();
    }

    public void TurnOff() {
        endOfFightScreen.SetActive(false);
    }

    public void TurnOn() {
        endOfFightScreen.SetActive(true);
    }

}
