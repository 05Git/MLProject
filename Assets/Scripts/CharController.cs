using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    private float m_Speed;
    private bool m_CanAct;
    private bool m_WaitingCanAct;
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Quaternion m_Rotation = Quaternion.identity;
    private float m_Horizontal;
    private float m_Vertical;
    private bool m_AttackP;
    private bool m_AttackK;
    private bool m_AttackUB;
    private bool m_Block;
    private int m_RoundsWon;
    [SerializeField]
    private Text m_Rounds_UI;

    public Transform target;
    public GameObject AttackPPoint;
    public GameObject AttackKPoint;
    public GameObject AttackUBPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
        m_CanAct = false;
        m_WaitingCanAct = false;
        m_RoundsWon = 0;
    }

    // Update is called once per frame
    void Update()
    {
        StateScript.State currentState = GetComponent<StateScript>().GetCurrentState();

        if (currentState == StateScript.State.Hitstun)
        {
            m_CanAct = false;
            m_Speed = 1.1f;
            m_Movement.Set(-0f, 0f, (-1f * m_Speed) * Time.deltaTime);
            transform.Translate(m_Movement);
            if (m_WaitingCanAct == false)
            {
                m_Animator.SetTrigger("HitstunTrigger");
                StartCoroutine(CanActDelay(0.4f));
            }
        }
        else if (currentState == StateScript.State.Blockstun)
        {
            m_CanAct = false;
            m_Speed = 0.7f;
            m_Movement.Set(-0f, 0f, (-1f * m_Speed) * Time.deltaTime);
            transform.Translate(m_Movement);
            if (m_WaitingCanAct == false)
            {
                m_Animator.SetTrigger("BlockstunTrigger");
                StartCoroutine(CanActDelay(0.2f));
            }
        }
        else if (currentState == StateScript.State.Win
            || currentState == StateScript.State.Lose)
        {
            m_CanAct = false;
        }

        if (m_CanAct)
        {
            if (m_AttackP)
            {
                GetComponent<StateScript>().SetCurrentState(StateScript.State.Attack);
                m_Block = false;
                m_AttackK = false;
                m_AttackUB = false;
                m_CanAct = false;
                m_Speed = 0f;
                m_Animator.SetTrigger("AttackPTrigger");
                if (m_WaitingCanAct == false)
                {
                    StartCoroutine(CanActDelay(0.4f));
                }
            }

            if (m_AttackK)
            {
                GetComponent<StateScript>().SetCurrentState(StateScript.State.Attack);
                m_Block = false;
                m_AttackUB = false;
                m_CanAct = false;
                m_Speed = 0f;
                m_Animator.SetTrigger("AttackKTrigger");
                if (m_WaitingCanAct == false)
                {
                    StartCoroutine(CanActDelay(0.9f));
                }
            }

            if (m_AttackUB)
            {
                GetComponent<StateScript>().SetCurrentState(StateScript.State.Attack);
                m_Block = false;
                m_CanAct = false;
                m_Speed = 0f;
                m_Animator.SetTrigger("AttackUBTrigger");
                if (m_WaitingCanAct == false)
                {
                    StartCoroutine(CanActDelay(1.3f));
                }
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

            if (!m_AttackK && !m_AttackP && !m_AttackUB && !m_Block)
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
                m_Speed = 2f;
            }
            
            bool isMoving = false;
            if (m_Horizontal != 0f || m_Vertical != 0f)
            {
                isMoving = true;
            }
            m_Animator.SetBool("Moving", isMoving);
            m_Animator.SetBool("Blocking", m_Block);

            m_Horizontal *= m_Speed;
            m_Vertical *= m_Speed;
            m_Movement.Set(-m_Vertical * Time.deltaTime, 0f, m_Horizontal * Time.deltaTime);
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
        m_WaitingCanAct = true;
        yield return new WaitForSeconds(time);
        m_CanAct = true;
        m_WaitingCanAct = false;
        GetComponent<StateScript>().SetCurrentState(StateScript.State.Idle);
    }

    void Activate_AttackKPoint()
    {
        AttackKPoint.SetActive(true);
    }

    void Deactivate_AttackKPoint()
    {
        if (AttackKPoint.activeInHierarchy)
        {
            AttackKPoint.SetActive(false);
        }
    }

    void Activate_AttackPPoint()
    {
        AttackPPoint.SetActive(true);
    }

    void Deactivate_AttackPPoint()
    {
        if (AttackPPoint.activeInHierarchy)
        {
            AttackPPoint.SetActive(false);
        }
    }
    void Activate_AttackUBPoint()
    {
        AttackUBPoint.SetActive(true);
    }

    void Deactivate_AttackUBPoint()
    {
        if (AttackUBPoint.activeInHierarchy)
        {
            AttackUBPoint.SetActive(false);
        }
    }

    public void AddRoundWin()
    {
        m_RoundsWon++;
        if (m_Rounds_UI != null)
        {
            m_Rounds_UI.text = string.Format("{0}", m_RoundsWon);
        }
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

    public void SetAttackK(bool val)
    {
        m_AttackK = val;
    }

    public void SetAttackP(bool val)
    {
        m_AttackP = val;
    }

    public void SetAttackUB(bool val)
    {
        m_AttackUB = val;
    }

    public void SetBlock(bool val)
    {
        m_Block = val;
    }

    public bool GetCanAct()
    {
        return m_CanAct;
    }

    public void SetCanAct(bool val)
    {
        m_CanAct = val;
    }
}
