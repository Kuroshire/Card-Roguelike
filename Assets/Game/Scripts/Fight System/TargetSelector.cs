using System;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private ShowTarget targetIndicator;
    private List<IFighter> validTarget = new();
    private int currentTargetIndex = 0;

    public event Action<IFighter> OnTargetConfirmed;

    public IFighter CurrentTarget => validTarget[currentTargetIndex];

    public bool IsTargeting {get; private set;}

    void Start()
    {
        StopTargeting();
    }

    public void StartTargeting(FighterTeam team) {
        GetAllPossibleTargets(team);
        if(validTarget.Count == 0) {
            Debug.Log("NO TARGET");
            throw new Exception("No target to attack");
        }

        currentTargetIndex = 0;
        targetIndicator.SetTarget(CurrentTarget);
        targetIndicator.TurnOn();
        gameObject.SetActive(true);
        IsTargeting = true;
    }
    
    public void StopTargeting() {
        targetIndicator.TurnOff();
        gameObject.SetActive(false);
        IsTargeting = false;
    }

    private void GetAllPossibleTargets(FighterTeam team) {
        List<IFighter> foundTargets = TurnBasedManager.TurnBasedFight.GetFightersFromTeam(team);
        validTarget = foundTargets;
    }

    private void GetAllPossibleTargets(List<FighterTeam> teamList) {
        List<IFighter> foundTargets = TurnBasedManager.TurnBasedFight.GetFightersFromMultipleTeams(teamList);
        validTarget = foundTargets;
    }

    #region INPUTS FUNCTIONS

    void Update()
    {
        if(validTarget.Count == 0) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)) {
            PreviousTarget();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)) {
            NextTarget();
        }

        if(Input.GetKeyDown(KeyCode.P)) {
            ConfirmTarget();
        }
    }

    public void NextTarget() {
        currentTargetIndex++;
        currentTargetIndex %= validTarget.Count;
        targetIndicator.SetTarget(CurrentTarget);
    }

    public void PreviousTarget() {
        currentTargetIndex = currentTargetIndex - 1 + validTarget.Count;
        currentTargetIndex %= validTarget.Count;
        targetIndicator.SetTarget(CurrentTarget);
    }

    public void ConfirmTarget() {
        StopTargeting();
        OnTargetConfirmed?.Invoke(CurrentTarget);
    }

    #endregion
}
