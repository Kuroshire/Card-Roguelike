using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private TargetSelector targetSelector;
    public static TurnBasedFight TurnBasedFight => Instance.turnBasedFight;
    public static TargetSelector TargetSelector => Instance.targetSelector;

    
    public static bool IsPlaying(IFighter givenFighter) {
        if(givenFighter != TurnBasedFight.CurrentFighter) {
            return false;
        }

        return true;
    }
}
