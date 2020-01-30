using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float m_Speed;
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
        GetComponent<StateScript>().SetState(StateScript.State.Idle);
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
        if (m_CanAct)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
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