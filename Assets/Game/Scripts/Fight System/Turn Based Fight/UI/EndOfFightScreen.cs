using TMPro;
using UnityEngine;

public class EndOfFightScreen : MonoBehaviour
{
    [SerializeField] private GameObject endOfFightScreen;
    [SerializeField] private TextMeshProUGUI winnerText;

    void Awake()
    {
        TurnOff();
    }

    void OnEnable()
    {
        TurnBasedEvents.OnFightOver += ActivateEndOfFight;
    }

    void OnDisable()
    {
        TurnBasedEvents.OnFightOver -= ActivateEndOfFight;
    }

    public void ActivateEndOfFight(TeamEnum winners) {

        switch(winners) {
            case TeamEnum.Player:
                winnerText.text = "Players Won !";
                break;
            case TeamEnum.Enemy:
                winnerText.text = "Monsters Won !";
                break;
            case TeamEnum.None:
                winnerText.text = "Draw !";
                break;
        }

        endOfFightScreen.SetActive(true);
    }

    public void TurnOff() {
        endOfFightScreen.SetActive(false);
    }
}
