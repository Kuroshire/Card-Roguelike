using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FighterActionButton : MonoBehaviour
{
    [SerializeField] IFighter fighter;
    [SerializeField] IFighter target;

    [SerializeField] bool isAutomaticFighter;

    void Start()
    {

        if(isAutomaticFighter) {
            TurnBasedManager.TurnBasedFight.OnCurrentFighterChange += AutomaticAttackAction;
            gameObject.SetActive(false);
        } else {
            SetButtonActive();
            TurnBasedManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
        }
    }

    private void AutomaticAttackAction() {
        if(TurnBasedManager.IsPlaying(fighter)) {
            fighter.Attack(target, 20);
        }
    }

    //used when button is pressed.
    public void FighterAttackAction() {
        if(TurnBasedManager.IsPlaying(fighter)) {
            fighter.Attack(target, 20);
        }
    }

    private void SetButtonActive() {
        bool isPlaying = TurnBasedManager.IsPlaying(fighter);

        gameObject.SetActive(isPlaying);
    }
}
