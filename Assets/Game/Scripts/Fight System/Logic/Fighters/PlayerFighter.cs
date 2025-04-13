using UnityEngine;

public class PlayerFighter: IFighter {
    public override FighterTeam Team { get => FighterTeam.Players; }

    public override void Attack(IFighter target, int damage) {
        target.TakeDamage(damage);
        Debug.Log("player attacked.");
        OnAttack?.Invoke();
    }
}
