using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Data", menuName = "Runes/Spells")]
public class SpellData : ScriptableObject
{
    [field: SerializeField] public List<RuneElement> RuneRecipe {get; private set;}
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public string Description {get; private set;}
    [field: SerializeField] public string Effect {get; private set;}

    public bool IsRecipeMatching(List<RuneElement> givenRecipe) {
        if(givenRecipe.Count == 0) {
            return false;
        }

        if(givenRecipe.Count != RuneRecipe.Count) {
            // Debug.Log("recipes are of different size, cannot be identical...");
            return false;
        }


        for(int i = 0; i < RuneRecipe.Count; i++) {
            if(RuneRecipe[i] != givenRecipe[i]) {
                return false;
            }
        }

        return true;
    }
}
