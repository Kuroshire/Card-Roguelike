using System.Collections;
using UnityEngine;

public class SpellSystemEntryPoint : MonoBehaviour
{
    void Start() {
        SpellSystemManager.DeckHandler.CreateDeckRandom(20);
        StartCoroutine(SpellSystemManager.DrawWithDelay(.5f));
    }
}
