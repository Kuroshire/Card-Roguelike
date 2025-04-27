using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayerActionButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private string cancelMessage = "Cancel";
    [SerializeField] private IFighterAttack attack;


    private string defaultMessage;
    private bool IsTargeting => FightSystemManager.TargetSelector.IsTargeting;

    void Start()
    {
        defaultMessage = buttonText.text;

        FightSystemManager.TurnBasedFight.OnFightStart += SetButtonActive;
        FightSystemManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
        FightSystemManager.TargetSelector.OnTargetConfirmed += AttackSelectedTarget;

        gameObject.SetActive(false);
    }

    //used when button is pressed.
    public void FighterAttackAction() {
        if(IsTargeting) {
            FightSystemManager.TargetSelector.StopTargeting();
        } else {
            try {
                FightSystemManager.TargetSelector.StartTargeting(FighterTeam.Monsters);
            } catch (Exception e) {
                //happens when there is no more targets.
                Debug.LogError(e);
                gameObject.SetActive(false);
            }
        }
        SetButtonText();
    }

    //This is a default action the player can do
    private void AttackSelectedTarget(IFighter target) {
        IFighter currentFighter = FightSystemManager.TurnBasedFight.GetCurrentFighter();
        if(CheckValidity(currentFighter, true)) {
            currentFighter.Attack(target, attack);
        }
    }

    private void SetButtonActive() {
        IFighter currentFighter = FightSystemManager.TurnBasedFight.GetCurrentFighter();
        bool isPlayerFighter = CheckValidity(currentFighter);
        gameObject.SetActive(isPlayerFighter);
        SetButtonText();
    }

    private void SetButtonText() {
        buttonText.text = IsTargeting ? cancelMessage : defaultMessage;
    }

    private bool CheckValidity(IFighter currentFighter, bool throwErrors = false) {
        if(currentFighter == null) {
            if(throwErrors)
                throw new Exception("Shouldn't be allowed to attack - There is no fight");

            return false;
        }
        if(currentFighter.Team != FighterTeam.Players) {
            if(throwErrors)
                throw new Exception("Current Fighter isn't a player, you shouldn't be allow to attack.");

            return false;
        }
        return true;
    }
}
