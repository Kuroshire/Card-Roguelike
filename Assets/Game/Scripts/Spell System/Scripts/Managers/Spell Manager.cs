using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [field: SerializeField] public SpellCollection UsableSpells {get; private set;}

    [SerializeField] private HandManager handManager;

    public SpellData FindValidSpell(List<RuneElement> runes) {
        foreach(SpellData spell in UsableSpells.SpellList) {
            if(spell.IsRecipeMatching(runes)) {
                Debug.Log("can use this spell: " + spell.Name);
                return spell;
            }
        }

        return null;
    }

    public SpellData FindValidSpellFromSelectedRunes() {
        List<RuneElement> selectedRunes = handManager.GetElementsFromSelectedRunes();
        return FindValidSpell(selectedRunes);
    }
}
