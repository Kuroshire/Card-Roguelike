using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public List<IFighterAttack> attackList;

    public IFighter ChoosePlayerTargetRandomly() {
        List<IFighter> playerFighters = FightSystemManager.TurnBasedFight.GetFightersFromTeam(FighterTeam.Players);
        int randomIndex = Random.Range(0, playerFighters.Count);

        return playerFighters[randomIndex];

    }

    public IFighterAttack ChooseRandomAttack() {
        int randomIndex = Random.Range(0, attackList.Count);

        return attackList[randomIndex];
    }
}
