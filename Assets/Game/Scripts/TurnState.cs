using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState {
    Start,
    SelectAction,
    SelectTarget,
    End,
}

public interface ITurnState {
    public void Enter();
    public void Update();
    public void Exit();
}

public class TurnStateManagement : MonoBehaviour {

    private ITurnState currentState;

    private Dictionary<TurnState, ITurnState> states = new();

    void Awake()
    {
        states = new Dictionary<TurnState, ITurnState>
        {
            { TurnState.Start, new StartTurnState() },
            { TurnState.SelectAction, new SelectActionTurnState() },
            { TurnState.SelectTarget, new SelectTargetTurnState() },
            { TurnState.End, new EndTurnState() },
        };

        SetState(TurnState.Start); // Start in Menu state
    }

    void Update()
    {
        currentState.Update();
    }

    public void SetState(TurnState newState) {
        currentState?.Exit();
        currentState = states[newState];
        currentState.Enter();
    }
}

public class StartTurnState : ITurnState
{


    public StartTurnState() {
        
    }

    public void Enter() {
        Debug.Log("MenuState Enter");
    }

    public void Update() {

    }

    public void Exit() {
        Debug.Log("MenuState Exit");
    }
}

public class SelectActionTurnState : ITurnState
{


    public SelectActionTurnState() {
        
    }

    public void Enter() {
        Debug.Log("MenuState Enter");
    }

    public void Update() {
        
    }

    public void Exit() {
        Debug.Log("MenuState Exit");
    }
}

public class SelectTargetTurnState : ITurnState
{


    public SelectTargetTurnState() {
        
    }

    public void Enter() {
        Debug.Log("MenuState Enter");
    }

    public void Update() {
        
    }

    public void Exit() {
        Debug.Log("MenuState Exit");
    }
}

public class EndTurnState : ITurnState
{


    public EndTurnState() {
        
    }

    public void Enter() {
        Debug.Log("MenuState Enter");
    }

    public void Update() {
        
    }

    public void Exit() {
        Debug.Log("MenuState Exit");
    }
}
