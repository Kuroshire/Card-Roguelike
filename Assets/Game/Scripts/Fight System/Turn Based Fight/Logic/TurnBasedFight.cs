using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnBasedFight: MonoBehaviour
{
    private List<IFighter> fighters = new();
    [SerializeField] private float timeBetweenTurns = .5f;

    private bool isFightOnGoing = false;

    private int currentFighterIndex = 0;
    private bool currentFighterPlayed = false;

    public event Action OnCurrentFighterChange;
    public event Action OnFightStart, OnFightOver;

    public Vector3 CurrentFighterPosition => GetCurrentFighter().transform.position;
    public FighterTeam WinningTeam {get; private set;} = FighterTeam.None;

    private bool isInitialized = false;
    public void InitializeFight() {
        fighters = FightSystemManager.FighterHandler.AllFighters;
        isInitialized = true;
    }

    /// <summary>
    /// return current fighter. If fighter isn't started yet, it initialize the fight.
    /// </summary>
    /// <returns></returns>
    public IFighter GetCurrentFighter() {
        if(!isInitialized) {
            Debug.Log("Fight isn't initialized !");
            InitializeFight();
        }

        return fighters[currentFighterIndex];
    }

    public void StartFight()
    {
        if(!isInitialized) {
            InitializeFight();
        }

        if(isFightOnGoing) {
            Debug.Log("fight already started...");
            return;
        }

        foreach(IFighter fighter in fighters) {
            SetFighterActions(fighter);
        }

        Debug.Log("fight started...");
        OnFightStart?.Invoke();

        StartCoroutine(FightLoop());
    }

    public void EndFight() {
        Debug.Log("Fight is over !");
        isFightOnGoing = false;
        isInitialized = false;

        OnFightOver?.Invoke();
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

        EndFight();
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
        Debug.Log(GetCurrentFighter() + " just attacked...");
        currentFighterPlayed = true;
    }

    private void RemoveDeadFighters() {
        List<IFighter> deadFighters= fighters.FindAll(fighter => fighter.IsAlive() == false);
        foreach(IFighter fighter in deadFighters) {
            UnsetFighterActions(fighter);
            fighters.Remove(fighter);
        }

    }

    private void SetFighterActions(IFighter fighter) {
        fighter.OnAttack += MakeMove;
        fighter.OnFighterDeath += RemoveDeadFighters;
    }

    private void UnsetFighterActions(IFighter fighter) {
        fighter.OnAttack -= MakeMove;
        fighter.OnFighterDeath -= RemoveDeadFighters;
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
