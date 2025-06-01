using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUser: MonoBehaviour {
    [SerializeField] RuneUseDisplay runeDisplay;
    [SerializeField] HandCardsPositionHandler handCardsPositionHandler;

    public event Action<List<RuneView>> OnSelectedCardUsed;
    public event Action<SpellData> OnSpellUse;
    public event Action OnSpellFailed;

    public void Initialise()
    {
        OnSelectedCardUsed += handCardsPositionHandler.UpdateHandPosition;
    }

    public void UseSpell()
    {
        List<RuneView> selectedRuneList = SpellSystemManager.CardManager.GetSelectedRuneList();
        List<RuneElement> selectedRuneElements = selectedRuneList.ConvertAll((runeView) => runeView.RuneElement);

        SpellData foundSpell = SpellSystemManager.SpellManager.FindValidSpellFromSelectedRunes();

        if (foundSpell)
        {
            UseCards(selectedRuneList);
            StartCoroutine(UseSpellCoroutine(selectedRuneElements, () =>
            {
                OnSpellUse?.Invoke(foundSpell);
            }));
        }
        else
        {
            OnSpellFailed?.Invoke();
        }
    }

    #region Use Spell On Target
    public void UseSpellOn(IFighter target)
    {
        List<RuneView> selectedRuneList = SpellSystemManager.CardManager.GetSelectedRuneList();
        List<RuneElement> selectedRuneElements = selectedRuneList.ConvertAll((runeView) => runeView.RuneElement);

        SpellData foundSpell = SpellSystemManager.SpellManager.FindValidSpellFromSelectedRunes();
        UseCards(selectedRuneList);

        StartCoroutine(UseSpellCoroutine(selectedRuneElements, () =>
        {
            if (foundSpell == null)
            {
                //no matching spells...
                Debug.Log("no spell found");
                OnSpellFailed?.Invoke();
                return;
            }

            SpellData spellToUse = foundSpell;
            IFighterAttack attackToUse = GetAttackFromSpell(spellToUse);

            FightSystemManager.TurnBasedFight.CurrentFighter.Attack(target, attackToUse);
            OnSpellUse?.Invoke(foundSpell);
        }));
    }

    private IFighterAttack GetAttackFromSpell(SpellData spell) {
        IFighterAttack attackFound = GameCollections.SpellToAttackCollection.GetFighterAttack(spell);
        if(attackFound == null) {
            Debug.Log("No attack found...");
        }

        return attackFound;
    }
    #endregion
    
    private IEnumerator UseSpellCoroutine(List<RuneElement> runeList, Action value)
    {
        yield return runeDisplay.DisplayRuneList(runeList);
        Debug.Log("FINISHED INCANTATION !");
        yield return new WaitForSeconds(.25f);
        value.Invoke();
    }

    private void UseCards(List<RuneView> runeViews)
    {
        runeViews.ForEach((card) =>
        {
            card.Use();
        });

        OnSelectedCardUsed?.Invoke(runeViews);
    }
}
