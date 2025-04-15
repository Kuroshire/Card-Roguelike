using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune Collection", menuName = "Collection/Rune Collection")]
public class RuneCollection : ScriptableObject
{
    [field: SerializeField] public List<RuneData> RuneList {get; private set;}
}
