using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuneRecipeShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI runeRecipe;
    [SerializeField] private TextMeshProUGUI spellFound;
    [SerializeField] private HandManager handManager;
    [SerializeField] private SpellManager spellManager;

    void Start()
    {
        handManager.OnHandSelectionChanges += UpdateTextWithSelection;
    }

    private void UpdateTextWithSelection() {
        List<RuneElement> selectionList = handManager.GetElementsFromSelectedRunes();
        UpdateTextWithRuneList(selectionList);
        UpdateTextWithSpellFound(selectionList);
    }


    private void UpdateTextWithRuneList(List<RuneElement> runes) {
        if(runes.Count == 0) {
            runeRecipe.text = "";
            return;
        }

        string newText = "";
        for(int i = 0; i < runes.Count; i++) {
            newText += $"{runes[i]} + ";
        }

        newText = newText[..^3];

        runeRecipe.text = newText;
    }

    private void UpdateTextWithSpellFound(List<RuneElement> runes) {
        string prefix = "Spell : ";
        List<SpellData> foundSpells = spellManager.FindValidSpell(runes);

        Debug.Log("number of spell found: " + foundSpells.Count);

        if(foundSpells.Count == 0) {
            //no matching spells...
            spellFound.text = prefix + "???";
            return;
        }

        spellFound.text = prefix + foundSpells[0].Name;
    }
}
