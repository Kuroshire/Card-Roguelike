using DG.Tweening;
using UnityEngine;

public class SpellFightSystemEntryPoint : MonoBehaviour {
    void Awake()
    {
        InitializeFight();
        InitalizeSpell();
    }

    private void InitalizeSpell() {
        SpellSystemManager.DeckHandler.CreateDeckRandom(20);
        StartCoroutine(SpellSystemManager.DrawWithDelay(1f));
    }

    private void InitializeFight() {
        FightSystemManager.FighterHandler.MovePlayersIntoFight();
        Invoke(nameof(StartFight), 1f);
    }
    
    private void StartFight() {
        Debug.Log("starting fight...");
        FightSystemManager.TurnBasedFight.StartFight();
    }
}
