using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnBasedFight : MonoBehaviour
{
    [Header("Referred Systems")]
    [SerializeField] private FighterHandler fighterHandler;
    [SerializeField] private FightSystemUIManager UIManager;

    [Header("Fight Settings")]
    [SerializeField] private float startOfTurnWaitTime = .5f, endOfTurnWaitTime = .5f, onFightEndWaitTime = .5f;
    // private int amountOfPlayers, amountOfEnemies;

    // --- Class Variables ---
    private bool isInitialized = false;
    private bool isFightOnGoing = false;
    private bool currentTurnFinished = false;
    private TeamEnum WinningTeam = TeamEnum.None;

    #region ACCESSORS
    public Vector3 CurrentFighterPosition => CurrentFighter ? CurrentFighter.transform.position : new(0, 0, 0);
    public IFighter CurrentFighter { get; private set; }
    public FighterTeam PlayerTeam => fighterHandler.PlayerTeam;
    public FighterTeam MonsterTeam => fighterHandler.MonsterTeam;
    public List<IFighter> AllFighters => fighterHandler.AllFighters;
    #endregion

    public void PrepareFight(FightSettings settings)
    {
        fighterHandler.PrepareFighters(settings.amountOfPlayers, settings.amountOfEnemies);
        // amountOfPlayers = settings.amountOfPlayers;
        // amountOfEnemies = settings.amountOfEnemies;
        isInitialized = true;
    }

    public void StartFight()
    {
        if (!isInitialized)
        {
            // PrepareFight();
            throw new Exception("cannot start fight before Instantiation...");
        }

        if (isFightOnGoing)
        {
            Debug.Log("fight already started...");
            return;
        }
        AssignEndTurn();

        StartCoroutine(StartFightCoroutine());
    }

    private void EndTurn(IFighter fighter)
    {
        if (fighter == CurrentFighter)
        {
            currentTurnFinished = true;
        }
    }

    private void EndFight()
    {
        Debug.Log("Fight is over !");
        isFightOnGoing = false;
        isInitialized = false;

        UnbindEndTurn();

        TurnBasedEvents.OnFightOver?.Invoke(WinningTeam);
    }

    private void AssignEndTurn()
    {
        foreach (IFighter fighter in AllFighters)
        {
            fighter.OnFighterEndTurn += EndTurn;
        }
    }

    private void UnbindEndTurn()
    {
        foreach (IFighter fighter in AllFighters)
        {
            fighter.OnFighterEndTurn -= EndTurn;
        }
    }

    #region FIGHT COROUTINES

    public IEnumerator StartFightCoroutine()
    {
        yield return StartCoroutine(FightEntranceCoroutine());
        yield return StartCoroutine(FightLoop());
        yield return StartCoroutine(EndFightCoroutine());
    }

    private IEnumerator FightEntranceCoroutine()
    {
        // UIManager.Initialize();

        fighterHandler.MovePlayersIntoFight();
        yield return new WaitForSeconds(fighterHandler.TimeToMoveIntoFight + .1f);

        Debug.Log("starting fight...");
        TurnBasedEvents.OnFightStart?.Invoke();
    }

    private IEnumerator FightLoop()
    {
        isFightOnGoing = true;

        while (IsFightOver() == false)
        {
            //Find current fighter
            CurrentFighter = fighterHandler.GetNextFighter(CurrentFighter);
            TurnBasedEvents.OnCurrentFighterChange?.Invoke();

            currentTurnFinished = false;
            TurnBasedEvents.OnTurnStart?.Invoke();
            yield return new WaitForSeconds(startOfTurnWaitTime);

            //Wait for player action
            yield return new WaitUntil(() => currentTurnFinished);

            TurnBasedEvents.OnTurnEnd?.Invoke();
            yield return new WaitForSeconds(endOfTurnWaitTime);
        }
    }

    private IEnumerator EndFightCoroutine()
    {
        Debug.Log("End Of Fight !");
        yield return new WaitForSeconds(onFightEndWaitTime);
        EndFight();
    }

    private bool IsFightOver()
    {

        bool hasMonsterLost = fighterHandler.MonsterTeam.IsTeamDead();
        bool hasPlayerLost = fighterHandler.PlayerTeam.IsTeamDead();

        if (hasPlayerLost)
        {
            WinningTeam = TeamEnum.Enemy;
            return true;
        }
        if (hasMonsterLost)
        {
            WinningTeam = TeamEnum.Player;
            return true;
        }

        return false;
    }

    #endregion
}
