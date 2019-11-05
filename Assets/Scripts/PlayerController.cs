using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walk_Normal,
        Attack,
        Hitstun,
        Block,
        Walk_Block,
        Blockstun,
        Grab_Start,
        Grab_Success,
        Grab_Whiff,
        Grabstun,
        Win,
        Lose
    };

    private enum Event
    {
        Enter,
        Active,
        Exit
    };

    private float m_Health;
    private float m_Speed;
    private State m_CharState;
    private State m_NextState;
    private Event m_StateEvent;
    private bool m_CanAct;
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Quaternion m_Rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        m_Health = 100.0f;
        m_CharState = State.Idle;
        m_StateEvent = Event.Enter;
        m_CanAct = true;
    }

    IEnumerator CanActDelay(float time)
    {
        yield return new WaitForSeconds(time);
        m_CanAct = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool attack = Input.GetKeyDown("mouse 0");
        bool block = Input.GetKey("mouse 1");
        bool grab = Input.GetKeyDown("left shift");

        if (m_CanAct)
        {
            if (grab)
            {
                m_CharState = State.Grab_Start;
                attack = false;
                block = false;
                m_CanAct = false;
                m_Speed = 1.0f;
                StartCoroutine(CanActDelay(0.35f));
            }

            if (attack)
            {
                m_CharState = State.Attack;
                block = false;
                m_CanAct = false;
                m_Speed = 1.0f;
                StartCoroutine(CanActDelay(0.5f));
            }

            if (block)
            {
                if (horizontal != 0 || vertical != 0)
                {
                    m_CharState = State.Walk_Block;
                }
                else
                {
                    m_CharState = State.Block;
                }
                m_CanAct = true;
                m_Speed = 2.8f;
            }

            if (!grab && !attack && !block)
            {
                if (horizontal != 0 || vertical != 0)
                {
                    m_CharState = State.Walk_Normal;
                }
                else
                {
                    m_CharState = State.Idle;
                }
                m_CanAct = true;
                m_Speed = 5.0f;
            }

            horizontal *= m_Speed;
            vertical *= m_Speed;
            m_Movement.Set(horizontal * Time.deltaTime, 0.0f, vertical * Time.deltaTime);
            transform.Translate(m_Movement);
        }

        /*
        if (m_CharState == State.Idle)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_Speed = 0.0f;
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {
                    
                if (horizontal != 0 || vertical != 0)
                {
                    m_NextState = State.Walk_Normal;
                    m_StateEvent = Event.Exit;
                }
                if (Input.GetKey("Mouse 1") == true)
                {
                    m_NextState = State.Block;
                    m_StateEvent = Event.Exit;
                }
            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Walk_Normal)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_Speed = 5.0f;
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {
                if (horizontal != 0 || vertical != 0)
                {
                    horizontal *= m_Speed;
                    vertical *= m_Speed;
                    m_Movement.Set(horizontal * Time.deltaTime, 0.0f, vertical * Time.deltaTime);
                    transform.Translate(m_Movement);
                }
                else
                {
                    m_NextState = State.Idle;
                    m_StateEvent = Event.Exit;
                }
            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Attack)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Hitstun)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Block)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_Speed = 0.0f;
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Walk_Block)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_Speed = 2.5f;
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {
                m_Movement.Set(horizontal * Time.deltaTime, 0.0f, vertical * Time.deltaTime);
                transform.Translate(m_Movement);
            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Blockstun)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Grab_Start)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Grab_Success)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Grab_Whiff)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Grabstun)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Win)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        else if (m_CharState == State.Lose)
        {
            if (m_StateEvent == Event.Enter)
            {
                m_StateEvent = Event.Active;
            }
            else if (m_StateEvent == Event.Active)
            {

            }
            else if (m_StateEvent == Event.Exit)
            {
                m_CharState = m_NextState;
                m_StateEvent = Event.Enter;
            }
        }
        */

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}