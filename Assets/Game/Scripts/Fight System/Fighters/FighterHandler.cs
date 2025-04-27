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

    public void MovePlayersIntoFight() {
        foreach(IFighter player in playerTeam) {
            playerFightPosition.MoveFighterIntoFight(player, timeToMoveIntoFight);
        }
    }
}
