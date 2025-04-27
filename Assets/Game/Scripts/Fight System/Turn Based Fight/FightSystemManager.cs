using System.Collections;
using UnityEngine;

public class FightSystemManager : MonoBehaviour
{
    #region Singleton

    public static FightSystemManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private TurnBasedFight turnBasedFight;
    [SerializeField] private FighterHandler fighterHandler;
    [SerializeField] private TargetSelector targetSelector;
    
    public static TurnBasedFight TurnBasedFight => Instance.turnBasedFight;
    public static FighterHandler FighterHandler => Instance.fighterHandler;
    public static TargetSelector TargetSelector => Instance.targetSelector;
    
    public static bool IsPlaying(IFighter givenFighter) {
        if(givenFighter != TurnBasedFight.GetCurrentFighter()) {
            return false;
        }
        return true;
    }

    public static bool IsFighterPlayer(IFighter fighter) {
        if(fighter.Team != FighterTeam.Players) {
            return false;
        }
        return true;
    }
    
    public static IEnumerator StartFight() {
        FighterHandler.MovePlayersIntoFight();
        yield return new WaitForSeconds(FighterHandler.TimeToMoveIntoFight + .1f);

        Debug.Log("starting fight...");
        TurnBasedFight.StartFight();
    }
}
