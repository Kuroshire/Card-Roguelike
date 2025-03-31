using System;
using UnityEngine;

public class CollectionGameManager: MonoBehaviour {
    #region Singleton

    public static CollectionGameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    #endregion

    public RuneCollection allRunes;
    public SpellCollection allSpells;

    public static Sprite GetSpriteFromRuneElement(RuneElement runeElement) {
        RuneData runeData = Instance.allRunes.RuneList.Find((currentRune) => currentRune.Element == runeElement);
        if(!runeData) {
            throw new Exception("The rune you are trying to access doesn't exist...: " + runeElement);
        }

        return runeData.Sprite;

    }


}
