using UnityEngine;

public class FightSystemManager : MonoBehaviour
{
    #region Singleton

    public static FightSystemManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    #region SUB SYSTEMS
    [SerializeField] private TurnBasedFight turnBasedFight;
    [SerializeField] private TargetSelector targetSelector;
    [SerializeField] private FighterFactory fighterFactory;

    public static TurnBasedFight TurnBasedFight => Instance.turnBasedFight;
    public static TargetSelector TargetSelector => Instance.targetSelector;
    public static FighterFactory FighterFactory => Instance.fighterFactory;
    #endregion

    public bool isInitialized = false;

    public static void Initialise(FightSettings settings)
    {
        TurnBasedFight.PrepareFight(settings);
    }

    //Assign this to a button.
    public void StartFight()
    {
        TurnBasedFight.StartFight();
    }

    //TODO: restart the fight entirely, respawning players and monsters like they were at the start of the fight.
    public void RestartFight()
    {

    }

    //TODO: create the next fight, but keep current players as is (same hp etc). Instantiate new monsters.
    public void GoToNextFight()
    {

    }

    #region UTILS
    public static bool IsPlaying(IFighter givenFighter)
    {
        if (givenFighter != TurnBasedFight.CurrentFighter)
        {
            return false;
        }
        return true;
    }

    public static bool IsFighterPlayer(IFighter fighter)
    {
        if (fighter.Team != TeamEnum.Player)
        {
            return false;
        }
        return true;
    }
    #endregion
}
