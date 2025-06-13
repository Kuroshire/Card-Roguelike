using UnityEngine;

public class FollowCurrentFighter : MonoBehaviour
{
    private TurnBasedFight turnBasedFight;

    [SerializeField] private GameObject[] children;

    void Awake()
    {
        TurnBasedEvents.OnFightStart += TurnOn;
        TurnBasedEvents.OnFightOver += TurnOff;
        TurnBasedEvents.OnCurrentFighterChange += UpdateCurrentFighter;
    }

    void Start()
    {
        turnBasedFight = FightSystemManager.TurnBasedFight;
        TurnOff();
    }

    void OnDestroy()
    {
        TurnBasedEvents.OnFightStart -= TurnOn;
        TurnBasedEvents.OnFightOver -= TurnOff;
        TurnBasedEvents.OnCurrentFighterChange -= UpdateCurrentFighter;
    }

    public void UpdateCurrentFighter()
    {
        transform.position = turnBasedFight.CurrentFighterPosition;
    }

    private void TurnOff(TeamEnum _ = TeamEnum.None) {
        foreach(GameObject child in children) {
            child.SetActive(false);
        }
        gameObject.SetActive(false);
    }

    private void TurnOn() {
        foreach(GameObject child in children) {
            child.SetActive(true);
        }
        gameObject.SetActive(true);

        UpdateCurrentFighter();
    }
}
