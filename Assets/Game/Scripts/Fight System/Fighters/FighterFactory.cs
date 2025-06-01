using UnityEngine;

public class FighterFactory : MonoBehaviour {
    [SerializeField] private PlayerFighter playerFighterPrefab;
    [SerializeField] private MonsterFighter monsterFighterPrefab;
    [SerializeField] private Transform playerFighterParent, monsterFighterParent;

    public PlayerFighter CreatePlayer(Vector3 initialPosition) {
        PlayerFighter player = Instantiate(playerFighterPrefab, playerFighterParent);
        player.transform.position = initialPosition;

        return player;
    }
    
    public MonsterFighter CreateMonster(Vector3 initialPosition) {
        MonsterFighter monster = Instantiate(monsterFighterPrefab, monsterFighterParent);
        monster.transform.position = initialPosition;

        return monster;
    }

    public bool DestroyFighter(IFighter fighter) {
        Destroy(fighter.gameObject);
        return true;
    }
}
