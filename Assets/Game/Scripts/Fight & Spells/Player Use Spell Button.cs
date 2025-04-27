using System;
using TMPro;
using UnityEngine;

public class PlayerUseSpellButton : MonoBehaviour
{
    [SerializeField] private PlayerFighter fighter;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private string cancelMessage = "Cancel";
    private string defaultMessage;

    private bool IsTargeting => FightSystemManager.TargetSelector.IsTargeting;
    
    void Start()
    {
        defaultMessage = buttonText.text;
        SetButtonActive();

        FightSystemManager.TurnBasedFight.OnCurrentFighterChange += SetButtonActive;
        FightSystemManager.TargetSelector.OnTargetConfirmed += AttackSelectedTarget;
        
        SpellSystemManager.SpellFightManager.OnSpellFailed += CancelTargeting;
    }

    //This is a default action the player can do
    private void AttackSelectedTarget(IFighter target) {
        SpellSystemManager.SpellFightManager.UseSpellOn(target);
    }

    private void SetButtonActive() {
        bool isPlaying = FightSystemManager.IsPlaying(fighter);
        gameObject.SetActive(isPlaying);
        SetButtonText();
    }

    private void CancelTargeting() {
        FightSystemManager.TargetSelector.StopTargeting();
        SpellSystemManager.HandManager.UnlockSelection();
    }

    public void OnButtonClick() {
        if(!FightSystemManager.IsPlaying(fighter)) {
            Debug.Log("Cannot attack now...");
            return;
        }

        if(IsTargeting) {
            CancelTargeting();
        } else {
            try {
                FightSystemManager.TargetSelector.StartTargeting(FighterTeam.Monsters);
                SpellSystemManager.HandManager.LockSelection();                
            } catch (Exception) {
                //happens when there is no more targets.
                gameObject.SetActive(false);
            }
        }
        SetButtonText();
    }
    
    private void SetButtonText() {
        buttonText.text = IsTargeting ? cancelMessage : defaultMessage;
    } 
}
