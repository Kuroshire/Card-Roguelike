using System;
using UnityEngine;

public class GameCollections : MonoBehaviour
{
    #region Singleton

    public static GameCollections Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] private RuneCollection allRunes;
    [SerializeField] private SpellCollection allSpells;
    [SerializeField] private SpellDataToAttackCollection spellDataToAttackCollection;

    public static RuneCollection AllRunes => Instance.allRunes;
    public static SpellCollection AllSpells => Instance.allSpells;
    public static SpellDataToAttackCollection SpellToAttackCollection => Instance.spellDataToAttackCollection;
}
