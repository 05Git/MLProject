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
        Win,
        Lose
    };
    
    private float m_Speed;
    private State m_CharState;
    private State m_NextState;
    private bool m_CanAct;
    private Vector3 m_Movement;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private Quaternion m_Rotation = Quaternion.identity;

    public Transform target;
    public GameObject attack1Point;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        m_CharState = State.Idle;
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
        float horizontal = Input.GetAxis("Vertical");
        float vertical = Input.GetAxis("Horizontal");
        bool attack = Input.GetKeyDown("mouse 0");
        bool block = Input.GetKey("mouse 1");
        //bool isMoving = !Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f);
        //m_Animator.SetBool("Moving", isMoving);

        if (m_CanAct)
        {
            if (attack)
            {
                m_CharState = State.Attack;
                block = false;
                m_CanAct = false;
                m_Speed = 1.0f;
                m_Animator.SetTrigger("Attack1Trigger");
                StartCoroutine(CanActDelay(0.78f));
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

            if (!attack && !block)
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
            m_Movement.Set(-horizontal * Time.deltaTime, 0.0f, vertical * Time.deltaTime);
            transform.Translate(m_Movement);
            if (target != null)
            {
                transform.LookAt(target);
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
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
}