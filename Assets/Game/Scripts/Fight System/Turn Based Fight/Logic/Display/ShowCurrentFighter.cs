using System.Collections;
using System.Collections.Generic;
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
        transform.position = fightHandler.GetCurrentFighter().transform.position;
    }

    private void TurnOff() {
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        gameObject.SetActive(true);
    }
}
