using System;
using System.Collections;
using UnityEngine;

public class MonsterFighter: IFighter {

    [SerializeField] private int turnsBetweenAttacks;
    [SerializeField] private int turnsLeft;

    [SerializeField] private IFighter target;
    [SerializeField] private int attackDamage;

    public int TurnsLeftBeforeAttack => turnsLeft;

    public event Action OnTurnsLeftUpdate;

    public override FighterTeam Team { get => FighterTeam.Monsters; }

    void Start()
    {
        SetTurnsLeft(turnsBetweenAttacks);
        TurnBasedManager.TurnBasedFight.OnCurrentFighterChange += AutomaticAttackAction;
        OnFighterDeath += DeathBehaviour;

    }

    public void SetTurnsLeft(int turnsLeft) {
        this.turnsLeft = turnsLeft;
        OnTurnsLeftUpdate?.Invoke();
    }

    public override void Attack(IFighter target, int damage) {
        StartCoroutine(ThinkingTime(target, damage));
    }

    private void AutomaticAttackAction() {
        if(TurnBasedManager.IsPlaying(this)) {
            StartCoroutine(ThinkingTime(target, attackDamage));
        }
    }

    private IEnumerator ThinkingTime(IFighter target, int damage) {
        yield return new WaitForSeconds(.5f);
        AttackBehaviour(target, damage);
    }

    private void AttackBehaviour(IFighter target, int damage) {
        if(turnsLeft > 0) {
            Debug.Log("monster won't attack this turn...");
            SetTurnsLeft(turnsLeft - 1);
            OnAttack?.Invoke();
            return;
        }

        DefaultAttackBehaviour(target, damage);
        SetTurnsLeft(turnsBetweenAttacks);
    }

    private void DeathBehaviour() {
        gameObject.SetActive(false);
    }
}
