using UnityEngine;

public class FightPosition : MonoBehaviour {
    [SerializeField] private Transform fightPosition;
    private IFighter fighter;

    public Vector3 Position => fightPosition.position;

    public bool SetFighter(IFighter fighter) {
        if(IsAvailable()) {
            this.fighter = fighter;
            return true;
        }
        
        return false;
    }

    public bool IsAvailable() {
        if(fighter.IsAlive()) {
            return false;
        }
        return true;
    }
}
