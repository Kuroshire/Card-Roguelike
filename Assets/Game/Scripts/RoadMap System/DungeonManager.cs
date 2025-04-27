using UnityEngine;

public class DungeonManager : MonoBehaviour {
    public Dungeon dungeon;

    public int dungeonDepth, dungeonWidth; // how many room the player can go through, and how many rooms there can be in total per row.
    public int minNextChoicesPerRoom, maxNextChoicesPerRoom;

    void Start()
    {
        dungeon = new Dungeon(dungeonDepth, dungeonWidth);
    }
}
