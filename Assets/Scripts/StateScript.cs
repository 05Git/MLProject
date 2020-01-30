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

    private State m_CharState;

    public State GetCurrentState()
    {
        return m_CharState;
    }

    public void SetState(State state)
    {
        m_CharState = state;
    }
}
