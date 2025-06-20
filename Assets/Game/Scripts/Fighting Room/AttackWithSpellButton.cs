using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackWithSpellButton : MonoBehaviour
{
    [SerializeField] private TargetSelector targetSelector;
    [SerializeField] private TurnBasedFight turnBasedFight;
    [SerializeField] private SpellManager spellManager;
    [SerializeField] private SpellUser spellUser;


    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private string cancelMessage = "Cancel";


    private string defaultMessage;
    private bool IsTargeting => targetSelector.IsTargeting;

    void Awake()
    {
        defaultMessage = buttonText.text;
        targetSelector.OnTargetConfirmed += AttackSelectedTarget;

        TurnBasedEvents.OnFightStart += SetButtonActive;
        TurnBasedEvents.OnCurrentFighterChange += SetButtonActive;
        TurnBasedEvents.OnFightOver += TurnOff;
    }


    private void OnDestroy()
    {
        TurnBasedEvents.OnFightStart -= SetButtonActive;
        TurnBasedEvents.OnCurrentFighterChange -= SetButtonActive;
        TurnBasedEvents.OnFightOver -= TurnOff;
    }

    //used when button is pressed.
    public void FighterAttackAction()
    {
        if (IsTargeting)
        {
            targetSelector.StopTargeting();
        }
        else
        {
            try
            {
                List<IFighter> targetList = turnBasedFight.MonsterTeam.Fighters;
                targetSelector.StartTargeting(targetList);
            }
            catch (Exception e)
            {
                //happens when there is no more targets.
                Debug.LogError(e);
                gameObject.SetActive(false);
            }
        }
        SetButtonText();
    }

    //This is a default action the player can do
    private void AttackSelectedTarget(IFighter target)
    {
        IFighter currentFighter = FightSystemManager.TurnBasedFight.CurrentFighter;

        if (CheckValidity(currentFighter, true))
        {
            spellUser.UseSpellOn(target);
        }
    }

    private void SetButtonActive() {
        Debug.Log("set Active");
        IFighter currentFighter = FightSystemManager.TurnBasedFight.CurrentFighter;
        bool isPlayerFighter = CheckValidity(currentFighter);
        gameObject.SetActive(isPlayerFighter);
        SetButtonText();
    }
    
    void TurnOff(TeamEnum team = TeamEnum.None)
    {
        gameObject.SetActive(false);
    }

    private void SetButtonText()
    {
        buttonText.text = IsTargeting ? cancelMessage : defaultMessage;
    }

    private bool CheckValidity(IFighter currentFighter, bool throwErrors = false) {
        if(currentFighter == null) {
            if(throwErrors)
                throw new Exception("Shouldn't be allowed to attack - There is no fight");

            return false;
        }
        if(currentFighter.Team != TeamEnum.Player) {
            if(throwErrors)
                throw new Exception("Current Fighter isn't a player, you shouldn't be allow to attack.");

            return false;
        }
        return true;
    }
}
