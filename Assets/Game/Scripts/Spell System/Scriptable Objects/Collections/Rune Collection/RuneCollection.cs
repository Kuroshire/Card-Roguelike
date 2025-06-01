using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rune Collection", menuName = "Collection/Rune Collection")]
public class RuneCollection : ScriptableObject
{
    [field: SerializeField] public List<RuneData> RuneList { get; private set; }

    public Sprite GetSpriteFromRuneElement(RuneElement element)
    {
        RuneData foundRuneData = RuneList.Find((runeData) => runeData.Element == element);
        if (foundRuneData == null)
        {
            throw new Exception($"Could not find the element {element} in collection... Please check the collection !");
        }

        return foundRuneData.Sprite;
    }
}
