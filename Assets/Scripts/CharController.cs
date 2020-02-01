using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private float m_Speed;
    private bool m_CanAct;
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Quaternion m_Rotation = Quaternion.identity;
    private float horizontal;
    private float vertical;
    private bool attack;
    private bool block;

    public Transform target;
    public GameObject attack1Point;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<StateScript>().SetNextState(StateScript.State.Idle);
        GetComponent<StateScript>().UpdateCurrentState();
        GetComponent<StateScript>().SetCurrentEvent(StateScript.Event.Enter);
        m_CanAct = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<StateScript>().UpdateCurrentState();
        StateScript.State currentState = GetComponent<StateScript>().GetCurrentState();

        if (currentState == StateScript.State.Hitstun)
        {
            m_CanAct = false;
            //m_Animator.SetTrigger("HitstunTrigger");
            StartCoroutine(CanActDelay(0.45f));
        }
        else if (currentState == StateScript.State.Blockstun)
        {
            m_CanAct = false;
            //m_Animator.SetTrigger("Blockstun1Trigger");
            StartCoroutine(CanActDelay(0.2f));
        }
        else if (currentState == StateScript.State.Win
            || currentState == StateScript.State.Lose
            || currentState == StateScript.State.Draw)
        {
            m_CanAct = false;
        }

        if (m_CanAct)
        {
            if (attack)
            {
                GetComponent<StateScript>().SetNextState(StateScript.State.Attack);
                block = false;
                m_CanAct = false;
                m_Speed = 1.0f;
                m_Animator.SetTrigger("Attack1Trigger");
                StartCoroutine(CanActDelay(0.9f));
            }

            if (block)
            {
                if (horizontal != 0 || vertical != 0)
                {
                    GetComponent<StateScript>().SetNextState(StateScript.State.Walk_Block);
                }
                else
                {
                    GetComponent<StateScript>().SetNextState(StateScript.State.Block);
                }
                m_CanAct = true;
                m_Speed = 0.8f;
            }

            if (!attack && !block)
            {
                if (horizontal != 0 || vertical != 0)
                {
                    GetComponent<StateScript>().SetNextState(StateScript.State.Walk_Normal);
                }
                else
                {
                    GetComponent<StateScript>().SetNextState(StateScript.State.Idle);
                }
                m_CanAct = true;
                m_Speed = 2.0f;
            }
            /*
            bool isMoving = false;
            if (horizontal != 0.0f || vertical != 0.0f)
            {
                isMoving = true;
            }
            //m_Animator.SetBool("Moving", isMoving);
            //m_Animator.SetBool("Blocking", block);
            */
            horizontal *= m_Speed;
            vertical *= m_Speed;
            m_Movement.Set(-vertical * Time.deltaTime, 0.0f, horizontal * Time.deltaTime);
            /*
            float distance = Vector3.Distance(transform.localPosition + m_Movement, target.localPosition);
            if (m_TargetRadius < distance)
            {
                
            }
            */
            transform.Translate(m_Movement);

            if (target != null)
            {
                transform.LookAt(target);
            }
        }
    }

    IEnumerator CanActDelay(float time)
    {
        yield return new WaitForSeconds(time);
        m_CanAct = true;
        if (GetComponent<StateScript>().GetCurrentState() == StateScript.State.Hitstun
            || GetComponent<StateScript>().GetCurrentState() == StateScript.State.Blockstun)
        {
            GetComponent<StateScript>().SetNextState(StateScript.State.Idle);
        }
    }

    void Activate_Attack1Point()
    {
        attack1Point.SetActive(true);
    }

    void Deactivate_Attack1Point()
    {
        if (attack1Point.activeInHierarchy)
        {
            attack1Point.SetActive(false);
        }
    }

    public void SetHorizontal(float val)
    {
        horizontal = val;
    }

    public void SetVertical(float val)
    {
        vertical = val;
    }

    public void SetAttack(bool val)
    {
        attack = val;
    }

    public void SetBlock(bool val)
    {
        block = val;
    }
}
