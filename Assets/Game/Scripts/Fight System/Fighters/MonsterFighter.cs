using System.Collections;
using UnityEngine;

public class MonsterFighter: IFighter {

    [SerializeField] private int turnsBeforeAttack;
    [SerializeField] private int turnsLeft;

    public override FighterTeam Team { get => FighterTeam.Monsters; }

    void Start()
    {
        turnsLeft = turnsBeforeAttack;
    }


    public override void Attack(IFighter target, int damage) {
        StartCoroutine(ThinkingTime(target, damage));
    }

    private IEnumerator ThinkingTime(IFighter target, int damage) {
        yield return new WaitForSeconds(.5f);
        AttackBehaviour(target, damage);
    }

    private void AttackBehaviour(IFighter target, int damage) {
        if(turnsLeft > 0) {
            Debug.Log("monster won't attack this turn...");
            turnsLeft--;
            OnAttack?.Invoke();
            return;
        }

        DefaultAttackBehaviour(target, damage);
        turnsLeft = turnsBeforeAttack;
    }
}
