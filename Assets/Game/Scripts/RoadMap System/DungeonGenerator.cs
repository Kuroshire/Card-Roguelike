using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class DungeonGenerator : MonoBehaviour
{
    public int numberOfStartRooms = 3;
    public int depth = 5; // Number of room layers, doesn't count boss.
    public int maxLayerWidth = 3;
    public int maxNextRoomChoices = 2;

    public List<List<DungeonRoom>> dungeonLayers = new();

    public Action OnDungeonGenerated;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon() {
        GenerateLayers();
        // GenerateConnections();
        Debug.Log("done generating !");
        Debug.Log(dungeonLayers.SelectMany( layer => layer).ToArray().Length);
        OnDungeonGenerated?.Invoke();
    }

    void GenerateLayers()
    {
        //generate first layer
        CreateLayer(numberOfStartRooms);

        for(int i = 1; i < depth; i++) {
            int numberOfRooms = UnityEngine.Random.Range(2, maxLayerWidth + 1);
            CreateLayer(numberOfRooms);
        }

        //Final layer: Boss
        CreateLayer(1);
    }

    private void CreateLayer(int numberOfRooms) {
        List<DungeonRoom> layer = new();
        for(int i = 0; i < numberOfRooms; i++) {
            DungeonRoom room = new();
            layer.Add(room);
        }

        dungeonLayers.Add(layer);
    }

    private void GenerateConnections() {
        //connect all rooms on topside together.
        List<DungeonRoom> firstRooms = dungeonLayers.Select(layer => layer.First()).ToList();
        for(int i = 0; i < firstRooms.Count - 1; i++) {
            firstRooms[i].nextRooms.Add(firstRooms[i + 1]);
        }

        List<DungeonRoom> lastRooms = dungeonLayers.Select(layer => layer.Last()).ToList();
        for(int i = 0; i < lastRooms.Count - 1; i++) {
            lastRooms[i].nextRooms.Add(lastRooms[i + 1]);
        }
    }
}
