using System;
using System.Collections;
using UnityEngine;

public class MonsterFighter: IFighter {

    [SerializeField] private int turnsBetweenAttacks;
    private int turnsLeft;

    [SerializeField] private MonsterBehaviour behaviour;

    public int TurnsLeftBeforeAttack => turnsLeft;

    public event Action OnTurnsLeftUpdate;

    public override TeamEnum Team { get => TeamEnum.Enemy; }

    void Start()
    {
        SetTurnsLeft(turnsBetweenAttacks);
        OnFighterDeath += DeathBehaviour;

    }

    void OnEnable()
    {
        TurnBasedEvents.OnCurrentFighterChange += AutomaticAttackAction;
    }

    void OnDisable()
    {
        TurnBasedEvents.OnCurrentFighterChange -= AutomaticAttackAction;
    }

    public void SetTurnsLeft(int turnsLeft)
    {
        this.turnsLeft = turnsLeft;
        OnTurnsLeftUpdate?.Invoke();
    }

    private void AutomaticAttackAction() {
        if(FightSystemManager.IsPlaying(this)) {
            StartCoroutine(ThinkingTime());
        }
    }

    private IEnumerator ThinkingTime() {
        IFighterAttack attack = behaviour.ChooseRandomAttack();
        IFighter target = behaviour.ChoosePlayerTargetRandomly();

        yield return new WaitForSeconds(.5f);
        Attack(target, attack);
    }

    public override void Attack(IFighter target, IFighterAttack attack) {
        if(turnsLeft > 0) {
            Debug.Log("monster won't attack this turn...");
            SetTurnsLeft(turnsLeft - 1);
            EndTurn();
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

        EndTurn();
    }

    private void DeathBehaviour(IFighter _) {
        gameObject.SetActive(false);
    }
}
