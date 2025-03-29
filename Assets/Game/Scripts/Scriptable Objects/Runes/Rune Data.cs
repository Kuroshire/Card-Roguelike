using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune Data", menuName = "Runes/Rune Data")]
public class RuneData : MonoBehaviour
{
    [field: SerializeField] public Sprite Sprite {get; private set;}
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public Elements Element {get; private set;}
    [field: SerializeField] public RuneRarity Rarity {get; private set;}
}

public enum Elements {
    FIRE, WATER, AIR, EARTH
}

public enum RuneRarity {
    SMALLER, GREATER, RARE, POWERFUL, MYTHICAL
}