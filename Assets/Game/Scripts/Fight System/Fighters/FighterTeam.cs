using System.Collections.Generic;

public class FighterTeam {
    private readonly List<IFighter> fighterTeam = new();

    public List<IFighter> Fighters => fighterTeam;

    public FighterTeam(List<IFighter> fighters) {
        fighterTeam = fighters;
    }

    public bool IsTeamDead() {
        foreach(IFighter fighter in fighterTeam) {
            if(fighter.IsAlive()) {
                return false;
            }
        }

        return true;
    }

    public void ClearTeam() {
        foreach(IFighter fighter in fighterTeam) {
            FightSystemManager.FighterFactory.DestroyFighter(fighter);
        }

        fighterTeam.Clear();
    }
}
