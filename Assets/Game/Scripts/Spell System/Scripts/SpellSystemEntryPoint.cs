using System.Collections;
using UnityEngine;

public class SpellSystemEntryPoint : MonoBehaviour
{
    [SerializeField] private int deckSize = 20, handSize = 7;
    [SerializeField] private float drawDelay = .5f;
    [SerializeField] private bool shouldStartImmediately = false;

    void Start()
    {
        if (shouldStartImmediately)
        {
            BeginSpellSystem();
        }
    }

    public void BeginSpellSystem()
    {
        Debug.Log("starting...");
        SpellSystemManager.Initialise(deckSize, handSize);
        StartCoroutine(SpellSystemManager.DrawHandWithDelay(drawDelay));
    }
}
