public class FighterStats {
    public int maxHP, currentHP;

    public FighterStats(int maxHP) {
        this.maxHP = maxHP;
        currentHP = maxHP;
    }

    public FighterStats(int maxHP, int currentHP) {
        this.maxHP = maxHP;
        this.currentHP = currentHP;
    }
}
