using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEditor.Animations;
using UnityEngine;

public class FollowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight fightHandler;

    [SerializeField] private GameObject[] children;

    void Start()
    {
        fightHandler = FightSystemManager.TurnBasedFight;

        fightHandler.OnFightStart += TurnOn;
        fightHandler.OnFightOver += TurnOff;
        fightHandler.OnCurrentFighterChange += UpdateCurrentFighter;
    }

    public void UpdateCurrentFighter() {
        transform.position = fightHandler.CurrentFighterPosition;
    }

    private void TurnOff() {
        foreach(GameObject child in children) {
            child.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        foreach(GameObject child in children) {
            child.SetActive(true);
        }
        gameObject.SetActive(true);

        UpdateCurrentFighter();
    }
}
