using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [field: SerializeField] public SpellCollection UsableSpells {get; private set;}

    [SerializeField] private HandManager handManager;

    public List<SpellData> FindValidSpell(List<RuneElement> runes) {
        List<SpellData> validSpells = new();

        foreach(SpellData spell in UsableSpells.SpellList) {
            if(spell.IsRecipeMatching(runes)) {
                Debug.Log("can use this spell: " + spell.Name);
                validSpells.Add(spell);
            }
        }

        return validSpells;
    }
}
