using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnBasedFight: MonoBehaviour
{
    [SerializeField] private float timeBetweenTurns = .5f;
    private bool isInitialized = false;
    private bool isFightOnGoing = false;
    private int currentFighterIndex = 0;
    private bool currentFighterPlayed = false;
    public FighterTeam WinningTeam {get; private set;} = FighterTeam.None;


    public Vector3 CurrentFighterPosition => GetCurrentFighter().transform.position;
    private List<IFighter> Fighters => FightSystemManager.FighterHandler.AllFighters;

    public event Action OnCurrentFighterChange;
    public event Action OnFightStart, OnFightOver;

    public void InitializeFight() {
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

        return Fighters[currentFighterIndex];
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

        foreach(IFighter fighter in Fighters) {
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
        while(WinningTeam == FighterTeam.None) {
            // Debug.Log("It's " + CurrentFighter + " turn...");
            yield return new WaitUntil(() => currentFighterPlayed);
            Debug.Log("-------------- TURN PLAYED ! --------------");
            yield return new WaitForSeconds(timeBetweenTurns);
            
            HandleEndOfTurn();
        }
    }

    private void HandleEndOfTurn() {
        if(IsFightOver()) {
            EndFight();
        } else {
            NextFighter();
        }
    }

    private bool IsFightOver() {

        bool hasMonsterLost = FightSystemManager.FighterHandler.IsMonsterTeamDead;
        bool hasPlayerLost = FightSystemManager.FighterHandler.IsPlayerTeamDead;

        if(hasPlayerLost) {
            WinningTeam = FighterTeam.Monsters;
            return true;
        }
        if(hasMonsterLost) {
            WinningTeam = FighterTeam.Players;
            return true;
        }

        return false;
    }

    private void NextFighter() {
        if(Fighters.Count == 0) {
            Debug.LogError("No targets to focus");
            return;
        }
        Debug.Log("next fighter move...");
        currentFighterIndex = (currentFighterIndex + 1) % Fighters.Count;
        currentFighterPlayed = false;
        OnCurrentFighterChange?.Invoke();
    }

    private void MakeMove() {
        Debug.Log(GetCurrentFighter() + " just attacked...");
        currentFighterPlayed = true;
    }

    private void RemoveDeadFighter(IFighter fighter) {
        List<IFighter> deadFighters= Fighters.FindAll(fighter => fighter.IsAlive() == false);
        UnsetFighterActions(fighter);
    }

    private void SetFighterActions(IFighter fighter) {
        fighter.OnAttack += MakeMove;
        fighter.OnFighterDeath += RemoveDeadFighter;
    }

    private void UnsetFighterActions(IFighter fighter) {
        fighter.OnAttack -= MakeMove;
        fighter.OnFighterDeath -= RemoveDeadFighter;
    }

    #endregion

    /// <summary>
    /// returns the list of fighters within the given team who are still alive.
    /// </summary>
    public List<IFighter> GetFightersFromTeam(FighterTeam team) {
        return Fighters.FindAll(fighter => fighter.IsAlive() && fighter.Team == team);
    }

    public List<IFighter> GetFightersFromMultipleTeams(List<FighterTeam> teamList) {
        return Fighters.FindAll(fighter => fighter.IsAlive() && teamList.Contains(fighter.Team));
    }
}

public enum FighterTeam {
    None,
    Players,
    Monsters,
}
