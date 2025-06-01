using UnityEngine;

public class Rune {
    
    [field: SerializeField] public string Name {get; private set;}
    [field: SerializeField] public RuneElement Element {get; private set;}
    [field: SerializeField] public RuneRarity Rarity {get; private set;}

    public Rune()
    {
        Element = RuneElementExtension.GetOneRandomElement();
        Name = Element + " rune";
        Rarity = RuneRarity.SMALLER;
    }

    public Rune(RuneData runeData)
    {
        Name = runeData.Name;
        Element = runeData.Element;
        Rarity = RuneRarity.SMALLER;
    }

    public void PerformEffect() {
        Debug.Log("using rune : " + Name);
    }

    public override string ToString() {
        return "Name : " + Name + ", Element : " + Element;
    }
}
