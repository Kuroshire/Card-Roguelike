using UnityEngine;

public class Rune {
    private readonly RuneData runeData;

    public Rune(RuneData runeData) {
        this.runeData = runeData;
        Rarity = runeData.Rarity;
    }

    public Sprite Sprite { get => runeData.Sprite; }
    public RuneElement Element { get => runeData.Element; }
    public string Title { get => runeData.Name; }
    public RuneRarity Rarity { get; set; }

    public void PerformEffect() {
        Debug.Log("using rune : " + Title);
    }

    public override string ToString() {
        return "Name : " + Title + ", Element : " + Element;
    }
}
