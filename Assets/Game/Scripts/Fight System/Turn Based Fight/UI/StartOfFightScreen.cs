using UnityEngine;

public class StartOfFightScreen : MonoBehaviour {
    [SerializeField] private GameObject startOfFightScreen;

    public void TurnOff() {
        startOfFightScreen.SetActive(false);
    }

    public void TurnOn() {
        startOfFightScreen.SetActive(true);
    }
}
