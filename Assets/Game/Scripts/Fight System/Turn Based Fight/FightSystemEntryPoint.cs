using DG.Tweening;
using UnityEngine;

public class FightSystemEntryPoint : MonoBehaviour {

    
    [SerializeField] PlayerFighter player;
    [SerializeField] Transform playerPosition;

    void Start()
    {
        player.transform.DOMove(playerPosition.position, 1f);
        Invoke(nameof(StartFight), 1f);
    }

    public void StartFight() {
        Debug.Log("starting fight...");
        FightSystemManager.TurnBasedFight.StartFight();
    }
}
