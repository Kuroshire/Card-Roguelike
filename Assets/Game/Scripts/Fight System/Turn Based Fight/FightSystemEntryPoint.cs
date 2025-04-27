using UnityEngine;

public class FightSystemEntryPoint : MonoBehaviour {

    [SerializeField] private bool shouldStartFightOnLoad = false;
    [SerializeField] private GameObject StartFightScreen;

    void Start()
    {
        StartFightScreen.SetActive(true);
        if(shouldStartFightOnLoad) {
            StartFightOnClick();
        }
    }

    //Assign this to a button.
    public void StartFightOnClick()
    {
        StartCoroutine(FightSystemManager.StartFight());
    }
}
