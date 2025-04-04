using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnBasedFight: MonoBehaviour
{
    [SerializeField] private List<IFighter> fighters = new();


    private bool isFightOnGoing = false;

    private int currentFighterIndex = 0;
    private bool currentFighterPlayed = false;

    public event Action OnCurrentFighterChange;
    public event Action OnFightOver;

    public IFighter CurrentFighter => fighters[currentFighterIndex];
    public FighterTeam WinningTeam {get; private set;} = FighterTeam.None;


    public void StartFight()
    {
        if(isFightOnGoing) {
            Debug.Log("fight already started...");
            return;
        }

        foreach(IFighter fighter in fighters) {
            fighter.OnAttack += MakeMove;
        }

        Debug.Log("fight started...");
        StartCoroutine(FightLoop());
    }

    public IEnumerator FightLoop() {
        isFightOnGoing = true;
        while(!IsFightOver()) {
            Debug.Log("It's " + CurrentFighter + " turn...");
            yield return new WaitUntil(() => currentFighterPlayed);
            Debug.Log("-------------- TURN PLAYED ! --------------");
            NextFighter();
        }

        Debug.Log("Fight is over !");
        OnFightOver?.Invoke();
        isFightOnGoing = false;
    }

    private bool IsFightOver() {
        int playerLeft = 0;
        int monstersLeft = 0;

        foreach(IFighter fighter in fighters) {
            if(fighter.IsAlive()) {
                Debug.Log("It's alive !");
                switch(fighter.Team) {
                    case FighterTeam.Players:
                        playerLeft++;
                        break;
                    case FighterTeam.Monsters:
                        monstersLeft++;
                        break;
                    default:
                        Debug.LogWarning("Unknown fighter !");
                        break;
                }
            }
        }

        if(playerLeft == 0) {
            WinningTeam = FighterTeam.Monsters;
            return true;
        }

        if(monstersLeft == 0) {
            WinningTeam = FighterTeam.Players;
            return true;
        }

        return false;
    }

    private void NextFighter() {
        // do {
            Debug.Log("next fighter move...");
            currentFighterIndex = (currentFighterIndex + 1) % fighters.Count;
            currentFighterPlayed = false;
            OnCurrentFighterChange?.Invoke();
        // } while(!CurrentFighter.IsAlive());
    }

    public void MakeMove() {
        Debug.Log(CurrentFighter + " just attacked...");
        currentFighterPlayed = true;
    }
}

public enum FighterTeam {
    None,
    Players,
    Monsters,
}
