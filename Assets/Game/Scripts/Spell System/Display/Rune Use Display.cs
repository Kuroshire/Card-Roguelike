using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUseDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer runeDisplayer;
    [SerializeField] private FightManager fightManager;

    void Start()
    {
        runeDisplayer.gameObject.SetActive(false);
        fightManager.OnSpellUse += ShowSpellRunes;
    }

    public void ShowSpellRunes(SpellData spell) {
        List<RuneElement> recipe = spell.RuneRecipe;

        StartCoroutine(DisplayRuneList(recipe));
    }

    private void SetRuneToDisplay(RuneElement element) {
        Sprite runeSprite = CollectionGameManager.GetSpriteFromRuneElement(element);
        runeDisplayer.sprite = runeSprite;
    }

    private void ShowRuneDisplay() {
        runeDisplayer.gameObject.SetActive(true);
    }

    private void HideRuneDisplay() {
        runeDisplayer.gameObject.SetActive(false);
    }

    private IEnumerator DisplayRuneList(List<RuneElement> runes) {
        foreach(RuneElement rune in runes) {
            SetRuneToDisplay(rune);
            ShowRuneDisplay();
            yield return new WaitForSeconds(.15f);
            HideRuneDisplay();
            yield return new WaitForSeconds(.05f);
        }
    }
}
