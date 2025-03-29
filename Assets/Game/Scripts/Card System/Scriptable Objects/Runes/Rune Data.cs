using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune Data", menuName = "Runes/Rune Data")]
public class RuneData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite {get; private set;}
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public RuneElement Element {get; private set;}
    [field: SerializeField] public RuneRarity Rarity {get; private set;}
}
