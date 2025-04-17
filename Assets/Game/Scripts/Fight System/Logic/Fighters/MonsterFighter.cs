using System;
using System.Collections;
using UnityEngine;

public class MonsterFighter: IFighter {

    [SerializeField] private int turnsBetweenAttacks;
    [SerializeField] private int turnsLeft;

    [SerializeField] private IFighter target;
    [SerializeField] private int attackDamage;
    public int AttackDamage => attackDamage;
    [SerializeField] private IFighterAttack attack;

    public int TurnsLeftBeforeAttack => turnsLeft;

    public event Action OnTurnsLeftUpdate;

    public override FighterTeam Team { get => FighterTeam.Monsters; }

    void Start()
    {
        SetTurnsLeft(turnsBetweenAttacks);
        FightSystemManager.TurnBasedFight.OnCurrentFighterChange += AutomaticAttackAction;
        OnFighterDeath += DeathBehaviour;

    }

    public void SetTurnsLeft(int turnsLeft) {
        this.turnsLeft = turnsLeft;
        OnTurnsLeftUpdate?.Invoke();
    }

    public override void Attack(IFighter target, IFighterAttack attack) {
        StartCoroutine(ThinkingTime(target, attack));
    }

    private void AutomaticAttackAction() {
        if(FightSystemManager.IsPlaying(this)) {
            StartCoroutine(ThinkingTime(target, attack));
        }
    }

    private IEnumerator ThinkingTime(IFighter target, IFighterAttack attack) {
        yield return new WaitForSeconds(.5f);
        AttackBehaviour(target, attack);
    }

    private void AttackBehaviour(IFighter target, IFighterAttack attack) {
        if(turnsLeft > 0) {
            Debug.Log("monster won't attack this turn...");
            SetTurnsLeft(turnsLeft - 1);
            OnAttack?.Invoke();
            return;
        }

        StartCoroutine(AttackAnimation(target, attack));
    }

    private IEnumerator AttackAnimation(IFighter target, IFighterAttack attack) {
        IFighterAttack instance = Instantiate(attack);
        instance.Initialize(this, target);
        int attackDamage = attack.Damage;
        yield return new WaitForSeconds(instance.AnimationTime);
        target.TakeDamage(attack.Damage);
        OnAttack?.Invoke();

        SetTurnsLeft(turnsBetweenAttacks);
    }

    private void DeathBehaviour() {
        gameObject.SetActive(false);
    }
}
