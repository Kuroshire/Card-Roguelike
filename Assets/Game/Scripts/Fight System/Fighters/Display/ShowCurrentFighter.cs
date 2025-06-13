using UnityEngine;

public class ShowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight fightHandler;

    void Awake()
    {
        TurnBasedEvents.OnCurrentFighterChange += UpdateCurrentFighter;
        TurnBasedEvents.OnFightOver += TurnOff;
    }

    void Start()
    {
        fightHandler = FightSystemManager.TurnBasedFight;
        
        TurnOn();
    }

    void OnDestroy()
    {
        TurnBasedEvents.OnCurrentFighterChange -= UpdateCurrentFighter;
        TurnBasedEvents.OnFightOver -= TurnOff;
    }


    public void UpdateCurrentFighter()
    {
        if (fightHandler.CurrentFighter == null)
        {
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
