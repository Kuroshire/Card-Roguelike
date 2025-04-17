using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FireballAttack : IFighterAttack
{
    [SerializeField] private float travelTime = .25f, scaleUpTime = .5f;
    public override float AnimationTime => travelTime + scaleUpTime + .1f;

    public override void Initialize(IFighter user, IFighter target) {
        transform.position = user.transform.position + Vector3.up;

        StartCoroutine(Animate(target));
    }

    private IEnumerator Animate(IFighter target) {
        transform.DOScale(.75f, scaleUpTime);
        yield return new WaitForSeconds(scaleUpTime);
        transform.DOMove(target.transform.position, travelTime);
        yield return new WaitForSeconds(travelTime);

        Destroy(gameObject);
    }
}
