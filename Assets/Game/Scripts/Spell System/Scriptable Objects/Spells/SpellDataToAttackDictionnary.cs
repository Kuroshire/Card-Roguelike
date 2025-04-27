using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell To Attack", menuName = "Collection/SpellToAttack")]
public class SpellDataToAttackDictionnary : SerializedScriptableObject {
    public Dictionary<SpellData, IFighterAttack> spellToAttackDict = new();

    public IFighterAttack GetFighterAttack(SpellData spell) {
        spellToAttackDict.TryGetValue(spell, out IFighterAttack attackFound);

        if (attackFound == null) {
            Debug.Log("This spell doesnt have an attack : " + spell.Name);
        }

        return attackFound;
    }
}
