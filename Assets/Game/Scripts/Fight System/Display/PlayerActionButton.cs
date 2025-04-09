using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerActionButton : MonoBehaviour
{
    [SerializeField] IFighter fighter;
    [SerializeField] IFighter target;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] string cancelMessage = "Cancel";

    private string defaultMessage;
    private bool IsTargeting => TurnBasedManager.TargetSelector.IsTargeting;

    void Start()
    {
        defaultMessage = buttonText.text;
        SetButtonActive();

        TurnBasedManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
        TurnBasedManager.TargetSelector.OnTargetConfirmed += AttackSelectedTarget;
    }

    //used when button is pressed.
    public void FighterAttackAction() {
        if(IsTargeting) {
            TurnBasedManager.TargetSelector.StopTargeting();
        } else {
            TurnBasedManager.TargetSelector.StartTargeting(FighterTeam.Monsters);
        }
        SetButtonText();
    }

    private void AttackSelectedTarget(IFighter target) {
        if(TurnBasedManager.IsPlaying(fighter)) {
            fighter.Attack(target, 20);
        }
    }

    private void SetButtonActive() {
        bool isPlaying = TurnBasedManager.IsPlaying(fighter);
        gameObject.SetActive(isPlaying);
        SetButtonText();
    }

    private void SetButtonText() {
        buttonText.text = IsTargeting ? cancelMessage : defaultMessage;
    } 
}
