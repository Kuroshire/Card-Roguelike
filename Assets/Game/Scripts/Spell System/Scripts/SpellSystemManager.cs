using System.Collections;
using UnityEngine;

public class SpellSystemManager : MonoBehaviour
{
    #region Singleton
    public static SpellSystemManager Instance { get; private set; }

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

    [SerializeField] private SpellUser spellUser;
    [SerializeField] private SpellManager spellManager;
    [SerializeField] private CardManager cardManager;
    [SerializeField] private SpellUIManager spellUIManager;

    public static SpellUser SpellUser => Instance.spellUser;
    public static CardManager CardManager => Instance.cardManager;
    public static SpellManager SpellManager => Instance.spellManager;
    public static SpellUIManager SpellUIManager => Instance.spellUIManager;

    public static bool Initialise(int deckSize, int handSize)
    {
        CardManager.Initialise(deckSize, handSize);
        SpellUIManager.Initialise();
        SpellUser.Initialise();

        return true;
    }

    public static IEnumerator DrawHandWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CardManager.FillHand();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CardManager.FillHand();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseHoverDetection.InteractWithHovered();
        }
    }
}
