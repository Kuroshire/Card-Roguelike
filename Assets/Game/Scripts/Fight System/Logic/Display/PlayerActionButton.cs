using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerActionButton : MonoBehaviour
{
    [SerializeField] protected PlayerFighter fighter;
    [SerializeField] protected TextMeshProUGUI buttonText;
    [SerializeField] protected string cancelMessage = "Cancel";
    [SerializeField] private IFighterAttack attack;


    protected string defaultMessage;
    protected bool IsTargeting => FightSystemManager.TargetSelector.IsTargeting;

    void Start()
    {
        defaultMessage = buttonText.text;
        SetButtonActive();

        FightSystemManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
        FightSystemManager.TargetSelector.OnTargetConfirmed += AttackSelectedTarget;
    }

    //used when button is pressed.
    public void FighterAttackAction() {
        if(IsTargeting) {
            FightSystemManager.TargetSelector.StopTargeting();
        } else {
            try {
                FightSystemManager.TargetSelector.StartTargeting(FighterTeam.Monsters);
            } catch (Exception) {
                //happens when there is no more targets.
                gameObject.SetActive(false);
            }
        }
        SetButtonText();
    }

    //This is a default action the player can do
    protected void AttackSelectedTarget(IFighter target) {
        if(FightSystemManager.IsPlaying(fighter)) {
            fighter.Attack(target, attack);
        }
    }

    protected void SetButtonActive() {
        bool isPlaying = FightSystemManager.IsPlaying(fighter);
        gameObject.SetActive(isPlaying);
        SetButtonText();
    }

    protected void SetButtonText() {
        buttonText.text = IsTargeting ? cancelMessage : defaultMessage;
    } 
}
