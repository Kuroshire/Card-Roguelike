using UnityEngine;

public class SpellDataToAttackCollection : MonoBehaviour {
    #region Singleton
    public static SpellDataToAttackCollection Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }
    }
    #endregion

    public SpellDataToAttackDictionnary dictionnary;

    public static IFighterAttack GetFighterAttack(SpellData spell) {
        return Instance.dictionnary.GetFighterAttack(spell);
    }


}
