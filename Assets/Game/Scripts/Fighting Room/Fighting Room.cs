using System;
using UnityEngine;

public class FightingRoom : MonoBehaviour
{
    [Header("Fight System Settings")]
    [SerializeField] int numberOfPlayer = 2;
    [SerializeField] int numberOfEnemies = 3;

    [Header("Entry Points")]
    [SerializeField] FightSystemEntryPoint fightSystem;
    [SerializeField] SpellSystemEntryPoint spellSystem;

    private bool isFightStarted = false;
    private Action OnRoomFightFinished;

    public void BeginFight()
    {
        if (isFightStarted)
        {
            return;
        }
        isFightStarted = true;

        FightSettings fightSettings = new(numberOfPlayer, numberOfEnemies);
        fightSystem.BeginFightWithSettings(fightSettings);
        spellSystem.BeginSpellSystem();
    }

    public void EndRoomFight()
    {
        isFightStarted = false;
        OnRoomFightFinished?.Invoke();
    }
}
