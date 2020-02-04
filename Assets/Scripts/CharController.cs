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
    private float m_Horizontal;
    private float m_Vertical;
    private bool m_Attack;
    private bool m_Block;
    private int m_RoundsWon;

    public Transform target;
    public GameObject attack1Point;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
        m_CanAct = false;
        m_RoundsWon = 0;
    }

    // Update is called once per frame
    void Update()
    {
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
            || currentState == StateScript.State.Lose)
        {
            m_CanAct = false;
        }

        if (m_CanAct)
        {
            if (m_Attack)
            {
                GetComponent<StateScript>().SetCurrentState(StateScript.State.Attack);
                m_Block = false;
                m_CanAct = false;
                m_Speed = 1.0f;
                m_Animator.SetTrigger("Attack1Trigger");
                StartCoroutine(CanActDelay(0.9f));
            }

            if (m_Block)
            {
                if (m_Horizontal != 0 || m_Vertical != 0)
                {
                    GetComponent<StateScript>().SetCurrentState(StateScript.State.Walk_Block);
                }
                else
                {
                    GetComponent<StateScript>().SetCurrentState(StateScript.State.Block);
                }
                m_CanAct = true;
                m_Speed = 0.8f;
            }

            if (!m_Attack && !m_Block)
            {
                if (m_Horizontal != 0 || m_Vertical != 0)
                {
                    GetComponent<StateScript>().SetCurrentState(StateScript.State.Walk_Normal);
                }
                else
                {
                    GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
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
            m_Horizontal *= m_Speed;
            m_Vertical *= m_Speed;
            m_Movement.Set(-m_Vertical * Time.deltaTime, 0.0f, m_Horizontal * Time.deltaTime);
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
            GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
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

    public void AddRoundWin()
    {
        m_RoundsWon++;
    }

    public int GetRoundsWon()
    {
        return m_RoundsWon;
    }

    public void SetHorizontal(float val)
    {
        m_Horizontal = val;
    }

    public void SetVertical(float val)
    {
        m_Vertical = val;
    }

    public void SetAttack(bool val)
    {
        m_Attack = val;
    }

    public void SetBlock(bool val)
    {
        m_Block = val;
    }

    public void SetCanAct(bool val)
    {
        m_CanAct = val;
    }
}
