using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Data", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite {get; private set;}
    [field: SerializeField] public int Cost {get; private set;}
    [field: SerializeField] public string Effect {get; private set;}
}
