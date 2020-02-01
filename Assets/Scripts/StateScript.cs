using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScript : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walk_Normal,
        Attack,
        Hitstun,
        Block,
        Walk_Block,
        Blockstun,
        Win,
        Lose,
        Draw
    };

    public enum Event
    {
        Enter,
        Update,
        Exit
    };

    private State m_CurrentState;
    private State m_NextState;
    private Event m_CurrentEvent;

    public State GetCurrentState()
    {
        return m_CurrentState;
    }

    public void UpdateCurrentState()
    {
        m_CurrentState = m_NextState;
    }

    public State GetNextState()
    {
        return m_NextState;
    }

    public void SetNextState(State state)
    {
        m_NextState = state;
    }

    public Event GetCurrentEvent()
    {
        return m_CurrentEvent;
    }

    public void SetCurrentEvent(Event val)
    {
        m_CurrentEvent = val;
    }
}
