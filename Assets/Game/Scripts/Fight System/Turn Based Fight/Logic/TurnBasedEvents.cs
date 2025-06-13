using System;

public static class TurnBasedEvents
{
    public static Action OnTurnStart;
    public static Action OnTurnEnd;
    public static Action OnCurrentFighterChange;
    public static Action OnFightStart;
    public static Action<TeamEnum> OnFightOver;
}
