using DG.Tweening;
using UnityEngine;

public class FighterArrival : MonoBehaviour
{   
    public static void MoveFighterIntoFight(IFighter player, float timeToMoveIntoFight,  Vector3 fightPosition) {
        // Vector3 fightPosition = new Vector2(fightPositionX.position.x, player.transform.position.y);
        player.transform.DOMove(fightPosition, timeToMoveIntoFight);
    }
}
