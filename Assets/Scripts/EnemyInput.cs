using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemyInput : MonoBehaviour
{
    private char m_State;
    private float m_State_Timer = 0f;
    private float m_RandChoice;

    public GameObject gameController;

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
        if (!gameController.GetComponent<GameController>().GetRoundStart()
            && !gameController.GetComponent<GameController>().GetRoundEnd())
        {
            if (m_State_Timer > 0f)
            {
                m_State_Timer -= Time.deltaTime;
            }
            else
            {
                UpdateState();
                m_State_Timer = 2f;
                m_RandChoice = Random.Range(0, 100);
            }
            Transform player = GetComponent<CharController>().target;
            StateScript.State playerState = player.GetComponent<StateScript>().GetCurrentState();
            float distance = Vector3.Distance(transform.localPosition, player.localPosition);
            if (m_State == 'A')
            {
                if (distance > 2.2f)
                {
                    GetComponent<CharController>().SetHorizontal(1.0f);
                    GetComponent<CharController>().SetVertical(0.0f);
                    GetComponent<CharController>().SetAttackP(false);
                    GetComponent<CharController>().SetAttackK(false);
                    GetComponent<CharController>().SetAttackUB(false);
                    GetComponent<CharController>().SetBlock(false);
                }
                else
                {
                    if (GetComponent<CharController>().target.GetComponent<CharController>().GetBlock())
                    {
                        if (m_RandChoice >= 0f && m_RandChoice < 10f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 10f && m_RandChoice < 20f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 20f && m_RandChoice < 30f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        else if (m_RandChoice >= 30f && m_RandChoice < 65f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(true);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 65f && m_RandChoice < 75f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(true);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 75f && m_RandChoice < 100f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(true);
                            GetComponent<CharController>().SetBlock(false);
                        }
                    }
                    else
                    {
                        if (m_RandChoice >= 0f && m_RandChoice < 10f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 10f && m_RandChoice < 20f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 20f && m_RandChoice < 30f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        if (m_RandChoice >= 30f && m_RandChoice < 55f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(true);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        if (m_RandChoice >= 55f && m_RandChoice < 90f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(true);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        if (m_RandChoice >= 90f && m_RandChoice < 100f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(true);
                            GetComponent<CharController>().SetBlock(false);
                        }
                    }
                }
            }
            else if (m_State == 'D')
            {
                if (distance < 2.2f)
                {
                    GetComponent<CharController>().SetHorizontal(-1.0f);
                    GetComponent<CharController>().SetVertical(0.0f);
                    GetComponent<CharController>().SetAttackP(false);
                    GetComponent<CharController>().SetAttackK(false);
                    GetComponent<CharController>().SetAttackUB(false);
                    GetComponent<CharController>().SetBlock(true);
                }
                else
                {
                    float m_RandChoice = Random.Range(0, 100);
                    if (GetComponent<CharController>().target.GetComponent<CharController>().GetBlock())
                    {
                        if (m_RandChoice >= 0f && m_RandChoice < 20f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            if (distance <= 2.4f)
                            {
                                GetComponent<CharController>().SetAttackP(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackP(false);
                            }
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 20f && m_RandChoice < 30f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 30f && m_RandChoice < 40f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        else if (m_RandChoice >= 40f && m_RandChoice < 50f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 50f && m_RandChoice < 60f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 70f && m_RandChoice < 80f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 80f && m_RandChoice < 90f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 90f && m_RandChoice < 95f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        else if (m_RandChoice >= 95f && m_RandChoice < 100f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                    }
                    else
                    {
                        if (m_RandChoice >= 0f && m_RandChoice < 10f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            if (distance <= 2.4f)
                            {
                                GetComponent<CharController>().SetAttackP(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackP(false);
                            }
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 10f && m_RandChoice < 20f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (distance <= 2.4f)
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 20f && m_RandChoice < 30f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 30f && m_RandChoice < 40f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(0.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        else if (m_RandChoice >= 40f && m_RandChoice < 50f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 50f && m_RandChoice < 60f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 70f && m_RandChoice < 80f)
                        {
                            GetComponent<CharController>().SetHorizontal(-1.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 80f && m_RandChoice < 90f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            if (GetComponent<CharController>().target.GetComponent<CharController>().GetAttack())
                            {
                                GetComponent<CharController>().SetAttackK(true);
                            }
                            else
                            {
                                GetComponent<CharController>().SetAttackK(false);
                            }
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(false);
                        }
                        else if (m_RandChoice >= 90f && m_RandChoice < 95f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(-1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                        else if (m_RandChoice >= 95f && m_RandChoice < 100f)
                        {
                            GetComponent<CharController>().SetHorizontal(0.0f);
                            GetComponent<CharController>().SetVertical(1.0f);
                            GetComponent<CharController>().SetAttackP(false);
                            GetComponent<CharController>().SetAttackK(false);
                            GetComponent<CharController>().SetAttackUB(false);
                            GetComponent<CharController>().SetBlock(true);
                        }
                    }
                }
            }
        }
        else
        {
            GetComponent<CharController>().SetHorizontal(0.0f);
            GetComponent<CharController>().SetVertical(0.0f);
            GetComponent<CharController>().SetAttackP(false);
            GetComponent<CharController>().SetAttackK(false);
            GetComponent<CharController>().SetAttackUB(false);
            GetComponent<CharController>().SetBlock(false);
        }
    }

    void UpdateState()
    {
        char[] weights = new char[100];
        float health = GetComponent<HealthScript>().GetHeath();
        float playerHealth = GetComponent<CharController>().target.GetComponent<HealthScript>().GetHeath();

        if (health >= playerHealth && health > 20f)
        {
            for (int i = 0; i < 65; i++)
            {
                weights[i] = 'A';
            }
            for (int i = 65; i < 100; i++)
            {
                weights[i] = 'D';
            }
        }
        else
        {
            for (int i = 0; i < 65; i++)
            {
                weights[i] = 'D';
            }
            if (health <= 20f)
            {
                for (int i = 65; i < 80; i++)
                {
                    weights[i] = 'D';
                }
                for (int i = 80; i < 100; i++)
                {
                    weights[i] = 'A';
                }
            }
            else
            {
                for (int i = 65; i < 100; i++)
                {
                    weights[i] = 'A';
                }
            }
        }

        m_State = weights[Random.Range(0, 100)];
    }
}
