using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Collection", menuName = "Collection/Card Collection")]
public class CardCollection : ScriptableObject
{
    [field: SerializeField] public List<CardData> CardList {get; private set;}
}
