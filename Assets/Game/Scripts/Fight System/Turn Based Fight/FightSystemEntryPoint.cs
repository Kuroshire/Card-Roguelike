using UnityEngine;

public class FightSystemEntryPoint : MonoBehaviour
{
    [SerializeField] private bool shouldStartImmediately = false;
    [SerializeField] private FightSettings settings;

    void Start()
    {
        if (shouldStartImmediately)
        {
            BeginFightSystem();
        }
    }

    public void SetSettings(FightSettings settings)
    {
        this.settings = settings;
    }

    public void BeginFightSystem()
    {
        Debug.Log("starting...");
        if (settings == null)
        {
            Debug.LogWarning("Settings are not set...");
            FightSettings defaultSettings = new(2, 3);
            SetSettings(defaultSettings);
        }
        FightSystemManager.Initialise(settings);
        FightSystemManager.Instance.StartFight();
    }

    public void BeginFightWithSettings(FightSettings settings)
    {
        SetSettings(settings);
        BeginFightSystem();
    }
}
