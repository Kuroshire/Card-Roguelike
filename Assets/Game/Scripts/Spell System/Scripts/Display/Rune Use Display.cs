using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUseDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer runeDisplayer;

    [SerializeField] private float runeShowTime = 0.2f, runeDownTime = 0.1f;

    void Start()
    {
        runeDisplayer.gameObject.SetActive(false);
        SpellFightManager.Instance.OnSelectedCardUsed += ShowSpellRunes;
    }

    public void ShowSpellRunes(List<RuneElement> runeList) {
        StartCoroutine(DisplayRuneList(runeList));
    }

    private void SetRuneToDisplay(RuneElement element) {
        Sprite runeSprite = GameCollections.GetSpriteFromRuneElement(element);
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
            yield return new WaitForSeconds(runeShowTime);
            HideRuneDisplay();
            yield return new WaitForSeconds(runeDownTime);
        }
    }
}
