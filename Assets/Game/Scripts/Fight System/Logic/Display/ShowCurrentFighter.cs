using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight fightHandler;

    void Awake()
    {
        fightHandler = TurnBasedManager.TurnBasedFight;
        fightHandler.OnCurrentFighterChange += UpdateCurrentFighter;
        fightHandler.OnFightOver += TurnOff;
        
        TurnOn();
    }


    public void UpdateCurrentFighter() {
        transform.position = fightHandler.CurrentFighter.transform.position;
    }

    private void TurnOff() {
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        gameObject.SetActive(true);
    }
}
