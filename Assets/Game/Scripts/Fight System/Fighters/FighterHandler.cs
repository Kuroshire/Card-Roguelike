using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

public class FighterHandler : MonoBehaviour
{
    [Header("Spawn positions")]
    [SerializeField] private Transform playerInitialPosition_X;
    [SerializeField] private FightPosition[] playerFightPositions, monsterFightPositions;
    
    [Header("Fighter Settings")]
    [SerializeField] private float timeToMoveIntoFight = 1f;

    // --- ACCESSORS ---
    public FighterTeam PlayerTeam { get; private set; }
    public FighterTeam MonsterTeam { get; private set; }
    public List<IFighter> AllFighters => PlayerTeam.Fighters.Concat(MonsterTeam.Fighters).ToList();
    public float TimeToMoveIntoFight => timeToMoveIntoFight;

    public void PrepareFighters(int amountOfPlayer, int amountOfMonsters) {
        if(amountOfPlayer > playerFightPositions.Length) {
            Debug.LogError("Cannot spawn this many players...");
            throw new Exception("Cannot spawn this many players...");
        }

        if(amountOfMonsters > monsterFightPositions.Length) {
            Debug.LogError("Cannot spawn this many monsters...");
            throw new Exception("Cannot spawn this many monsters...");
        }

        PlayerTeam?.ClearTeam();
        SetPlayerTeam(amountOfPlayer);

        MonsterTeam?.ClearTeam();
        SetMonsterTeam(amountOfMonsters);

    }

    private void SetPlayerTeam(int amountOfPlayer) {
        List<IFighter> playerFighterTeam = new();
        for(int i = 0; i < amountOfPlayer; i++) {
            IFighter newPlayer = FightSystemManager.FighterFactory.CreatePlayer(playerInitialPosition_X.position);
            playerFighterTeam.Add(newPlayer);
        }
        PlayerTeam = new(playerFighterTeam);
    }

    private void SetMonsterTeam(int amountOfMonsters) {
        List<IFighter> monsterFighterTeam = new();
        for(int j = 0; j < amountOfMonsters; j++) {
            IFighter newMonster = FightSystemManager.FighterFactory.CreateMonster(monsterFightPositions[j].Position);
            monsterFighterTeam.Add(newMonster);
        }
        MonsterTeam = new(monsterFighterTeam);
    }

    public void MovePlayersIntoFight() {
        for(int i = 0; i < PlayerTeam.Fighters.Count; i++) {
            FighterArrival.MoveFighterIntoFight(PlayerTeam.Fighters[i], timeToMoveIntoFight, playerFightPositions[i].Position);
        }
    }

    /// <summary>
    /// Handles the turn order logic. This function can 
    /// </summary>
    /// <param name="currentFighter">Current Fighter that is playing.</param>
    /// <returns></returns>
    public IFighter GetNextFighter(IFighter currentFighter) {
        // case fight just started and there is no current fighter yet.
        if(currentFighter == null) {
            return AllFighters[0];
        }

        int currentFighterIndex = AllFighters.FindIndex((fighter) => fighter == currentFighter);

        int nextFighterIndex = (currentFighterIndex + 1) % AllFighters.Count;
        while(AllFighters[nextFighterIndex].IsAlive() == false) {
            nextFighterIndex = (nextFighterIndex + 1) % AllFighters.Count;
        }

        return AllFighters[nextFighterIndex];
    }
}
