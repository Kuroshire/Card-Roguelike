using UnityEngine;

public class ShowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight fightHandler;

    void Start()
    {
        fightHandler = FightSystemManager.TurnBasedFight;
        fightHandler.OnCurrentFighterChange += UpdateCurrentFighter;
        fightHandler.OnFightOver += TurnOff;
        
        TurnOn();
    }


    public void UpdateCurrentFighter() {
        if(fightHandler.CurrentFighter == null) {
            TurnOff();
            return;
        }

        TurnOn();
        transform.position = fightHandler.CurrentFighter.transform.position;
    }

    private void TurnOff(TeamEnum _ = TeamEnum.None) {
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        gameObject.SetActive(true);
    }
}
