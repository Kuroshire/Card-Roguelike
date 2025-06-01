using System.Collections;
using UnityEngine;

public class PlayerFighter: IFighter {
    public override TeamEnum Team { get => TeamEnum.Player; }

    public override void Attack(IFighter target, IFighterAttack attack) {
        Debug.Log("player attacked.");
        StartCoroutine(AttackAnimation(target, attack));
    }

    private IEnumerator AttackAnimation(IFighter target, IFighterAttack attack) {
        IFighterAttack instance = Instantiate(attack);
        instance.Initialize(this, target);
        int attackDamage = attack.Damage;
        yield return new WaitForSeconds(instance.AnimationTime);
        
        ApplyDamage(target, attackDamage);

        EndTurn();
    }

    private void ApplyDamage(IFighter target, int attackDamage) {
        Debug.Log("dealing damage : " + attackDamage);
        target.TakeDamage(attackDamage);
        OnAttack?.Invoke();
    }
}
