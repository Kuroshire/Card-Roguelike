using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell Data", menuName = "Collection/Spell Collection")]
public class SpellCollection : ScriptableObject
{
    [field: SerializeField] public List<SpellData> SpellList {get; private set;}
}
