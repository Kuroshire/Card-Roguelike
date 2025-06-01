using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetInput : MonoBehaviour
{
    public TargetSelector TargetSelector => FightSystemManager.TargetSelector;

    [SerializeField] private KeyCode previousTargetKey = KeyCode.UpArrow, nextTargetKey = KeyCode.DownArrow, confirmTargetKey = KeyCode.P;

    // Update is called once per frame
    void Update()
    {
        if(TargetSelector.CanTarget) {
            return;
        }

        if(!TargetSelector.IsTargeting) {
            return;
        }

        if(Input.GetKeyDown(previousTargetKey)) {
            TargetSelector.PreviousTarget();
        }

        if(Input.GetKeyDown(nextTargetKey)) {
            TargetSelector.NextTarget();
        }

        if(Input.GetKeyDown(confirmTargetKey)) {
            TargetSelector.ConfirmTarget();
        }
    }
}
