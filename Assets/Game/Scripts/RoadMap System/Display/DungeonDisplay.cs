using System.Collections.Generic;
using UnityEngine;

public class DungeonDisplay : MonoBehaviour {

    [SerializeField] private Transform parentTransform;
    [SerializeField] private DungeonRoomDisplay roomDisplayPrefab;
    [SerializeField] private float gapBetweenLayers, gapBetweenRooms;

    public DungeonGenerator dungeon;

    void Awake()
    {
        dungeon.OnDungeonGenerated += InstantiateRoomDisplay;
    }

    public void InstantiateRoomDisplay() {
        Debug.Log("instantiating display....");
        int currentLayerIndex = 0;
        foreach(List<DungeonRoom> layer in dungeon.dungeonLayers) {
            float xPosition = parentTransform.position.x + gapBetweenLayers * currentLayerIndex;

            for(int i = 0; i < layer.Count; i++) {
                float yPosition = parentTransform.position.y + gapBetweenRooms * i;

                DungeonRoomDisplay newRoom = Instantiate(roomDisplayPrefab, parentTransform);
                newRoom.AssignRoom(layer[i]);
                newRoom.transform.position = new Vector2(xPosition, yPosition);
            }

            currentLayerIndex++;
        }
    }
}
