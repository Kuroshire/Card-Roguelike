using System;
using UnityEngine;

public class GameCollections: MonoBehaviour {
    #region Singleton

    public static GameCollections Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private RuneCollection allRunes;
    [SerializeField] private SpellCollection allSpells;

    public static RuneCollection AllRunes => Instance.allRunes;
    public static SpellCollection AllSpells => Instance.allSpells;

    public static Sprite GetSpriteFromRuneElement(RuneElement runeElement) {
        RuneData runeData = AllRunes.RuneList.Find((currentRune) => currentRune.Element == runeElement);
        if(!runeData) {
            throw new Exception("The rune you are trying to access doesn't exist...: " + runeElement);
        }

        return runeData.Sprite;

    }

}
