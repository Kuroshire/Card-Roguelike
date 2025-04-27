using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FightHandler : MonoBehaviour
{
    [SerializeField] PlayerFighter player;
    [SerializeField] Transform playerPosition;


    void Awake()
    {
        player.transform.DOMove(playerPosition.position, 1f);
        Invoke(nameof(StartFight), 1f);
    }

    public void StartFight() {
        FightSystemManager.TurnBasedFight.StartFight();
    }
}
