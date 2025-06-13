using UnityEngine;

public class FightSystemUIManager : MonoBehaviour {
    // [SerializeField] private StartOfFightScreen startOfFightScreen;
    [SerializeField] private EndOfFightScreen endOfFightScreen;

    public void Initialize()
    {
        endOfFightScreen.TurnOff();
        // startOfFightScreen.TurnOn();
    }
}
