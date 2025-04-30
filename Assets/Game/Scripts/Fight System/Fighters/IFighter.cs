using System;
using UnityEngine;

public abstract class IFighter : MonoBehaviour {
    [SerializeField] private int maxHP; 
    private int currentHP;
    private bool isAlive = true;

    public abstract FighterTeam Team {get;}
    public bool IsPlayerFighter => Team == FighterTeam.Players;
    
    public Action OnAttack;
    public Action OnCurrentHPChange;
    public Action<IFighter> OnFighterDeath;

    public int CurrentHP => currentHP;
    public int MaxHP => maxHP;

    private void Awake() {
        currentHP = maxHP;
    }
 
    public bool IsAlive() {
        return isAlive;
    }

    public abstract void Attack(IFighter target, IFighterAttack attack);


    public void TakeDamage(int amount) {
        currentHP -= amount;
        if(currentHP <= 0) {
            currentHP = 0;
            Die();
        }
        OnCurrentHPChange?.Invoke();
    }

    public void Die() {
        Debug.Log("fighter died...");
        isAlive = false;
        OnFighterDeath?.Invoke(this);

        Destroy(gameObject);
    }
}
