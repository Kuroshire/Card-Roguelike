using UnityEngine;

public class FightSystemUIManager : MonoBehaviour {
    // [SerializeField] private StartOfFightScreen startOfFightScreen;
    [SerializeField] private EndOfFightScreen endOfFightScreen;

    public void Initialize()
    {
        endOfFightScreen.TurnOff();
        endOfFightScreen.SetActionOnFightOver(FightSystemManager.TurnBasedFight);

        // startOfFightScreen.TurnOn();
    }
}
