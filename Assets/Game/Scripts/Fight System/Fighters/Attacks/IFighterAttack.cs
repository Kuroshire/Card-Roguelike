using UnityEngine;

public abstract class IFighterAttack : MonoBehaviour {
    [SerializeField] private int damage = 20;
    public int Damage => damage;
    public abstract float AnimationTime {get;}
    public abstract void Initialize(IFighter user, IFighter target);
}
