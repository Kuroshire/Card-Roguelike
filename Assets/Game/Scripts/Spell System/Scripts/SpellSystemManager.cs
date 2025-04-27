using System.Collections;
using UnityEngine;

public class SpellSystemManager : MonoBehaviour {
    #region Singleton
    public static SpellSystemManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
            InitializeManagers();
        }
    }
    #endregion

    [SerializeField] private SpellFightManager spellFightManager;
    [SerializeField] private SpellManager spellManager;
    [SerializeField] private HandManager handManager;
    [SerializeField] private DeckHandler deckHandler;

    private bool isInitialized = false;

    public void InitializeManagers() {
        isInitialized = true;
    }

    public static SpellFightManager SpellFightManager => Instance.spellFightManager;
    public static HandManager HandManager => Instance.handManager;
    public static SpellManager SpellManager => Instance.spellManager;
    public static DeckHandler DeckHandler => Instance.deckHandler;
    
    public static IEnumerator DrawWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        DeckHandler.DrawHand();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            DeckHandler.DrawHand();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            MouseHoverDetection.InteractWithHovered();
        }
    }
}
