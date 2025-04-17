using System.Collections;
using DG.Tweening;
using UnityEngine;

public class WaterfallAttack : IFighterAttack
{
    [SerializeField] private float travelTime = .25f, scaleUpTime = .25f, scaleDownTime = .5f;
    [SerializeField] private Transform startingPosition;
    public override float AnimationTime => travelTime + scaleUpTime + scaleDownTime + .1f;

    public override void Initialize(IFighter user, IFighter target) {
        transform.position = startingPosition.position;

        StartCoroutine(Animate(target));
    }

    private IEnumerator Animate(IFighter target) {
        transform.DOMove(target.transform.position, travelTime);
        yield return new WaitForSeconds(travelTime);

        transform.DOScale(1.5f, scaleUpTime);
        yield return new WaitForSeconds(scaleUpTime);

        transform.DOScale(0f, scaleDownTime);
        yield return new WaitForSeconds(scaleUpTime);

        Destroy(gameObject);
    }
}
