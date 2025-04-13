using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTarget : MonoBehaviour
{
    private IFighter currentTarget;

    public void SetTarget(IFighter fighter) {
        currentTarget = fighter;
        transform.position = currentTarget.transform.position;
    }

    public void TurnOff() {
        gameObject.SetActive(false);
    }

    public void TurnOn() {
        gameObject.SetActive(true);
    }
}
