using System.Collections;
using DG.Tweening;
using UnityEngine;

public class DefaultMonsterAttack : IFighterAttack
{
    [SerializeField] private float moveForwardTime = .25f, moveBackToPositionTime = .25f;
    public override float AnimationTime => moveForwardTime + moveBackToPositionTime + .1f;

    private Vector3 defaultPosition, forwardPosition;

    public override void Initialize(IFighter user, IFighter target) {
        if(user.GetType() != typeof(MonsterFighter)) {
            Debug.Log("user isn't a monster ???");
            return;
        }

        defaultPosition = user.transform.position;
        Vector3 direction = (target.transform.position - user.transform.position).normalized;
        forwardPosition = defaultPosition + 2 * direction;

        StartCoroutine(Animate(user));
    }

    private IEnumerator Animate(IFighter user) {
        user.transform.DOMove(forwardPosition, moveForwardTime);
        yield return new WaitForSeconds(moveForwardTime);
        user.transform.DOMove(defaultPosition, moveBackToPositionTime);
        yield return new WaitForSeconds(moveBackToPositionTime);

        Debug.Log("end of animation for monster default atk - DMG: " + Damage);

        Destroy(gameObject);
    }
}
