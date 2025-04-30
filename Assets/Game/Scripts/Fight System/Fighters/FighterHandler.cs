using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterHandler : MonoBehaviour
{
    [SerializeField] private FighterArrival playerFightPosition;
    [SerializeField] private List<PlayerFighter> playerTeam;
    [SerializeField] private List<MonsterFighter> monsterTeam;
    [SerializeField] private float timeToMoveIntoFight = 1f;
    public float TimeToMoveIntoFight => timeToMoveIntoFight;
    
    public List<IFighter> AllFighters => playerTeam.Concat<IFighter>(monsterTeam).ToList();

    //Check if there is at least one player still alive. If there is none, == null will return true, so IsPlayerTeamDead will be true.
    public bool IsPlayerTeamDead => playerTeam.Find((player) => player.IsAlive()) == null;
    public bool IsMonsterTeamDead => monsterTeam.Find((monster) => monster.IsAlive()) == null;

    private void Start() {
        foreach(IFighter fighter in AllFighters) {
            fighter.OnFighterDeath += RemoveOnDeath;
        }
    }

    public void MovePlayersIntoFight() {
        foreach(IFighter player in playerTeam) {
            playerFightPosition.MoveFighterIntoFight(player, timeToMoveIntoFight);
        }
    }

    private void RemoveOnDeath(IFighter fighter) {
        if(playerTeam.Contains(fighter)) {
            playerTeam.Remove((PlayerFighter) fighter);
        }

        if(monsterTeam.Contains(fighter)) {
            monsterTeam.Remove((MonsterFighter) fighter);
        }

        fighter.OnFighterDeath -= RemoveOnDeath;
    }
}
