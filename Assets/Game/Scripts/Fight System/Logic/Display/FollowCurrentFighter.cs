using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight fightHandler;

    void Start()
    {
        fightHandler = FightSystemManager.TurnBasedFight;

        fightHandler.OnFightStart += TurnOn;
        fightHandler.OnFightOver += TurnOff;
        fightHandler.OnCurrentFighterChange += UpdateCurrentFighter;
    }

    public void UpdateCurrentFighter() {
        transform.position = fightHandler.CurrentFighterPosition;
    }

    private void TurnOff() {
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        gameObject.SetActive(true);
    }
}
