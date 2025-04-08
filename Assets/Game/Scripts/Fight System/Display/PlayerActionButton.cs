using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerActionButton : MonoBehaviour
{
    [SerializeField] IFighter fighter;
    [SerializeField] IFighter target;

    void Start()
    {
        SetButtonActive();
        TurnBasedManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
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
