using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnBasedFight: MonoBehaviour
{
    [SerializeField] private List<IFighter> fighters = new();
    [SerializeField] private float timeBetweenTurns = .5f;

    private bool isFightOnGoing = false;

    private int currentFighterIndex = 0;
    private bool currentFighterPlayed = false;

    public event Action OnCurrentFighterChange;
    public event Action OnFightStart, OnFightOver;

    public IFighter CurrentFighter => fighters[currentFighterIndex];
    public Vector3 CurrentFighterPosition => CurrentFighter.transform.position;
    public FighterTeam WinningTeam {get; private set;} = FighterTeam.None;


    public void StartFight()
    {
        if(isFightOnGoing) {
            Debug.Log("fight already started...");
            return;
        }

        foreach(IFighter fighter in fighters) {
            fighter.OnAttack += MakeMove;
            fighter.OnFighterDeath += RemoveDeadFighters;
        }

        Debug.Log("fight started...");
        OnFightStart?.Invoke();

        StartCoroutine(FightLoop());
    }

    #region FIGHT LOGIC
    public IEnumerator FightLoop() {
        isFightOnGoing = true;
        while(!IsFightOver()) {
            // Debug.Log("It's " + CurrentFighter + " turn...");
            yield return new WaitUntil(() => currentFighterPlayed);
            Debug.Log("-------------- TURN PLAYED ! --------------");
            yield return new WaitForSeconds(timeBetweenTurns);
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

    private void MakeMove() {
        Debug.Log(CurrentFighter + " just attacked...");
        currentFighterPlayed = true;
    }

    private void RemoveDeadFighters() {
        List<IFighter> deadFighters= fighters.FindAll(fighter => fighter.IsAlive() == false);
        foreach(IFighter fighter in deadFighters) {
            fighters.Remove(fighter);
            fighter.OnFighterDeath -= RemoveDeadFighters;
        }

    }

    #endregion

    /// <summary>
    /// returns the list of fighters within the given team who are still alive.
    /// </summary>
    public List<IFighter> GetFightersFromTeam(FighterTeam team) {
        return fighters.FindAll(fighter => fighter.IsAlive() && fighter.Team == team);
    }

    public List<IFighter> GetFightersFromMultipleTeams(List<FighterTeam> teamList) {
        return fighters.FindAll(fighter => fighter.IsAlive() && teamList.Contains(fighter.Team));
    }
}

public enum FighterTeam {
    None,
    Players,
    Monsters,
}
