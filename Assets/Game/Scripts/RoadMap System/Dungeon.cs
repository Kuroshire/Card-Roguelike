using System.Collections.Generic;

public class Dungeon {

    public DungeonRoom currentRoom;
    public List<DungeonRoom> rooms;

    public int Depth {get; private set;}
    public int Width {get; private set;}

    public Dungeon(int depth, int width) {
        Depth = depth;
        Width = width;
        GenerateDungeon();
    }

    public void GenerateDungeon() {
        List<DungeonRoom> previousRooms = new();
        DungeonRoom entranceRoom = new();
        previousRooms.Add(entranceRoom);
        rooms.Add(entranceRoom);

        for(int i = 0; i < Depth; i++) {
            DungeonRoom nextRoom = new();
            previousRooms.ForEach(room => room.nextRooms.Add(nextRoom));
            previousRooms.Clear();
            previousRooms.Add(nextRoom);
            rooms.Add(nextRoom);
        }
    }

    public void SetAccessibleRooms() {
        foreach(DungeonRoom room in currentRoom.nextRooms) {
            room.status = RoomStatus.AVAILABLE;
        }
    }

    public void EnterRoom(DungeonRoom room) {
        if(room.status == RoomStatus.AVAILABLE) {
            currentRoom = room;
            room.Enter();
        }

        foreach(DungeonRoom otherRoom in rooms) {
            if(otherRoom.status == RoomStatus.AVAILABLE) {
                otherRoom.status = RoomStatus.LOCKED;
            }
        }
    }
}
