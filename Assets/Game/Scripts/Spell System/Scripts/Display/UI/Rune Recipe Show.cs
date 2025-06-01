using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RuneRecipeShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI runeRecipe;
    [SerializeField] private TextMeshProUGUI spellFound;
    private CardManager cardManager;
    private SpellManager spellManager;

    public void Initialise(CardManager cardManager, SpellManager spellManager)
    {
        this.cardManager = cardManager;
        this.spellManager = spellManager;
        cardManager.OnHandSelectionChange += UpdateTextWithSelection;
    }

    private void UpdateTextWithSelection()
    {
        // Debug.Log("updating display !");
        List<RuneElement> selectionList = cardManager.GetElementsFromSelectedRunes();
        UpdateTextWithRuneList(selectionList);
        UpdateTextWithSpellFound(selectionList);
    }


    private void UpdateTextWithRuneList(List<RuneElement> runes)
    {
        if (runes.Count == 0)
        {
            runeRecipe.text = "";
            return;
        }

        string newText = "";
        for (int i = 0; i < runes.Count; i++)
        {
            newText += $"{runes[i]} + ";
        }

        newText = newText[..^3];

        runeRecipe.text = newText;
    }

    private void UpdateTextWithSpellFound(List<RuneElement> runes)
    {
        string prefix = "Spell : ";
        SpellData foundSpell = spellManager.FindValidSpell(runes);

        if (foundSpell == null)
        {
            //no matching spells...
            spellFound.text = prefix + "???";
            return;
        }

        spellFound.text = prefix + foundSpell.Name;
    }
}
