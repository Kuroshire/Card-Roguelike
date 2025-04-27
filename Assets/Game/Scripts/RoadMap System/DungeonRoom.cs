using System.Collections;
using System.Collections.Generic;

public class DungeonRoom
{
    public RoomDifficulty difficulty;
    public RoomStatus status = RoomStatus.LOCKED;
    public List<DungeonRoom> nextRooms;

    public DungeonRoom() {

    }

    public void Enter() {
        status = RoomStatus.CURRENT;
    }

    public void Exit() {

    }
}

public enum RoomDifficulty {
    MONSTER, ELITE, BOSS, SHOP
}

public enum RoomStatus {
    CURRENT, AVAILABLE, LOCKED, COMPLETED, 
}
