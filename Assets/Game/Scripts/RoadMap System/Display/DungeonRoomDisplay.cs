using UnityEngine;
using UnityEngine.UI;

public class DungeonRoomDisplay : MonoBehaviour {
    DungeonRoom assignedRoom;
    [SerializeField] Image image;
    [SerializeField] Color currentRoomColor, availableRoomColor, lockedRoomColor, completedRoomColor;

    public void AssignRoom(DungeonRoom room) {
        assignedRoom = room;
        SetColorBasedOnStatus();
    }

    private void SetColorBasedOnStatus() {
        switch(assignedRoom.status) {
            case RoomStatus.CURRENT:
                image.color = currentRoomColor;
                break;
            
            case RoomStatus.AVAILABLE:
                image.color = availableRoomColor;
                break;
            
            case RoomStatus.LOCKED:
                image.color = lockedRoomColor;
                break;
            
            case RoomStatus.COMPLETED:
                image.color = completedRoomColor;
                break;
        }
    }
}