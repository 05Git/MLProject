using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    private bool m_CallAttack = false;
    private bool m_WaitingForAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharController>().SetHorizontal(0.0f);
        GetComponent<CharController>().SetVertical(0.0f);
        GetComponent<CharController>().SetAttackP(false);
        GetComponent<CharController>().SetAttackK(false);
        GetComponent<CharController>().SetAttackUB(false);
        GetComponent<CharController>().SetBlock(false);
    }

    // Update is called once per frame
    void Update()
    {
        Transform player = GetComponent<CharController>().target;
        StateScript.State playerState = player.GetComponent<StateScript>().GetCurrentState();
        float distance = Vector3.Distance(transform.localPosition, player.localPosition);
        if (distance > 3.0f)
        {
            GetComponent<CharController>().SetHorizontal(1.0f);
            GetComponent<CharController>().SetVertical(0.0f);
            GetComponent<CharController>().SetAttackP(false);
            GetComponent<CharController>().SetAttackK(false);
            GetComponent<CharController>().SetAttackUB(false);
            GetComponent<CharController>().SetBlock(false);
        }
        else if (distance < 3.0f)
        {
            if (playerState == StateScript.State.Block || playerState == StateScript.State.Walk_Block)
            {
                GetComponent<CharController>().SetHorizontal(0.0f);
                GetComponent<CharController>().SetVertical(0.0f);
                GetComponent<CharController>().SetAttackP(true);
                GetComponent<CharController>().SetAttackK(false);
                GetComponent<CharController>().SetAttackUB(false);
                GetComponent<CharController>().SetBlock(false);
            }
            else if (playerState == StateScript.State.Attack)
            {
                GetComponent<CharController>().SetHorizontal(0.0f);
                GetComponent<CharController>().SetVertical(-1.0f);
                GetComponent<CharController>().SetAttackP(false);
                GetComponent<CharController>().SetAttackK(false);
                GetComponent<CharController>().SetAttackUB(false);
                GetComponent<CharController>().SetBlock(true);
            }
            else if (playerState == StateScript.State.Idle || playerState == StateScript.State.Walk_Normal)
            {
                if (m_CallAttack == false)
                {
                    if (m_WaitingForAttack == false)
                    {
                        StartCoroutine(CallAttack(2.0f));
                    }
                    GetComponent<CharController>().SetHorizontal(0.0f);
                    GetComponent<CharController>().SetVertical(1.0f);
                    GetComponent<CharController>().SetAttackP(false);
                    GetComponent<CharController>().SetAttackK(false);
                    GetComponent<CharController>().SetAttackUB(false);
                    GetComponent<CharController>().SetBlock(false);
                }
                else
                {
                    GetComponent<CharController>().SetHorizontal(0.0f);
                    GetComponent<CharController>().SetVertical(0.0f);
                    GetComponent<CharController>().SetAttackP(false);
                    GetComponent<CharController>().SetAttackK(true);
                    GetComponent<CharController>().SetAttackUB(false);
                    GetComponent<CharController>().SetBlock(false);
                    m_CallAttack = false;
                }
            }
        }
    }

    IEnumerator CallAttack(float time)
    {
        m_WaitingForAttack = true;
        yield return new WaitForSeconds(time);
        m_CallAttack = true;
        m_WaitingForAttack = false;
    }
}
