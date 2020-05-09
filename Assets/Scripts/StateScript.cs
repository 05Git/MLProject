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
        Lose
    };

    private State m_CurrentState;

    public State GetCurrentState()
    {
        return m_CurrentState;
    }

    public void SetCurrentState(State state)
    {
        m_CurrentState = state;
    }

    public float[] GetStateAsFloat()
    {
        float[] state = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };
        if (m_CurrentState == State.Idle)
        {
            state[0] = 1f;
        }
        else if (m_CurrentState == State.Walk_Normal)
        {
            state[1] = 1f;
        }
        else if (m_CurrentState == State.Attack)
        {
            state[2] = 1f;
        }
        else if (m_CurrentState == State.Hitstun)
        {
            state[3] = 1f;
        }
        else if (m_CurrentState == State.Block)
        {
            state[4] = 1f;
        }
        else if (m_CurrentState == State.Walk_Block)
        {
            state[5] = 1f;
        }
        else if (m_CurrentState == State.Blockstun)
        {
            state[6] = 1f;
        }
        else if (m_CurrentState == State.Win)
        {
            state[7] = 1f;
        }
        else if (m_CurrentState == State.Lose)
        {
            state[8] = 1f;
        }
        return state;
    }
}
