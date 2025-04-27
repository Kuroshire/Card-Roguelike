using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpellFightManager: MonoBehaviour {
    [SerializeField] RuneUseDisplay runeDisplay;

    public event Action<List<RuneView>> OnSelectedCardUsed;
    public event Action<SpellData> OnSpellUse;
    public event Action OnSpellFailed;

    void Start()
    {
        OnSpellFailed += FailSpell;
    }

    public void UseSpellOn(IFighter target) {
        List<RuneView> selectedRuneList = SpellSystemManager.HandManager.GetSelectedRuneList();
        List<RuneElement> selectedRuneElements = selectedRuneList.ConvertAll((runeView) => runeView.RuneElement);
        
        SpellData foundSpell = SpellSystemManager.SpellManager.FindValidSpellFromSelectedRunes();
        UseCards(selectedRuneList);

        StartCoroutine(UseSpellCoroutine(selectedRuneElements, () => {
            if(foundSpell == null) {
                //no matching spells...
                Debug.Log("no spell found");
                OnSpellFailed?.Invoke();
                return;
            }

            SpellData spellToUse = foundSpell;
            IFighterAttack attackToUse = GetAttackFromSpell(spellToUse);

            FightSystemManager.TurnBasedFight.GetCurrentFighter().Attack(target, attackToUse);
            OnSpellUse?.Invoke(foundSpell);
        }));
    }

    private IEnumerator UseSpellCoroutine(List<RuneElement> runeList, Action value) {
        yield return runeDisplay.DisplayRuneList(runeList);
        Debug.LogWarning("finished incantation");
        yield return new WaitForSeconds(.25f);
        value.Invoke();
    }

    private IFighterAttack GetAttackFromSpell(SpellData spell) {
        IFighterAttack attackFound = SpellDataToAttackCollection.GetFighterAttack(spell);
        if(attackFound == null) {
            Debug.Log("No attack found...");
        }

        return attackFound;
    }

    private void FailSpell() {
        FightSystemManager.TurnBasedFight.GetCurrentFighter().OnAttack?.Invoke();
    }

    private void UseCards(List<RuneView> runeViews) {
        runeViews.ForEach((card) => {
            card.Use();
        });

        OnSelectedCardUsed?.Invoke(runeViews);
    }

}
