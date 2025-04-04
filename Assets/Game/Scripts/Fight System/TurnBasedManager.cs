using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedManager : MonoBehaviour
{
    #region Singleton

    public static TurnBasedManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private TurnBasedFight turnBasedFight;
    public static TurnBasedFight TurnBasedFight => Instance.turnBasedFight;

    void Start()
    {
        StartFight();
    }

    public void StartFight() {
        TurnBasedFight.StartFight();
    }

    public static bool IsPlaying(IFighter givenFighter) {
        if(givenFighter != TurnBasedFight.CurrentFighter) {
            Debug.Log("Current fighter is not : " + givenFighter);
            return false;
        }

        return true;
    } 
}
