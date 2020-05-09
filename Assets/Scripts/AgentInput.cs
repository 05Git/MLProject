using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class AgentInput : Agent
{
    private float m_InitialHealth;
    private float m_TargetInitialHealth;
    private bool m_HasWon;
    private bool m_HasLost;

    public GameObject target;
    public GameObject gameController;
    
    void Start()
    {
        m_InitialHealth = 100;
        m_TargetInitialHealth = 100;
        m_HasWon = false;
        m_HasLost = false;
        Monitor.SetActive(true);
    }

    public override void AgentReset()
    {
        m_HasWon = false;
        m_HasLost = false;
        m_InitialHealth = 100f;
        m_TargetInitialHealth = 100f;
    }

    public override void CollectObservations()
    {
        AddVectorObs(1 * ((this.transform.localPosition.x + 20f) / (-1f + 20f)));
        AddVectorObs(1 * ((this.transform.localPosition.z + 9.3f) / (9.3f + 9.3f)));
        AddVectorObs(1 * ((target.transform.localPosition.x + 20f) / (-1f + 20f)));
        AddVectorObs(1 * ((target.transform.localPosition.z + 9.3f) / (9.3f + 9.3f)));
        AddVectorObs(this.GetComponent<HealthScript>().GetHeath() / 100f);
        AddVectorObs(target.GetComponent<HealthScript>().GetHeath() / 100f);
        AddVectorObs(this.GetComponent<StateScript>().GetStateAsFloat());
        AddVectorObs(target.GetComponent<StateScript>().GetStateAsFloat());
    }

    public override void AgentAction(float[] vectorAction)
    {
        float horizontal = vectorAction[0];
        float vertical = vectorAction[1];
        float attackP = vectorAction[2];
        float attackK = vectorAction[3];
        float attackUB = vectorAction[4];
        float block = vectorAction[5];

        if (!gameController.GetComponent<GameController>().GetRoundStart()
            && !gameController.GetComponent<GameController>().GetRoundEnd())
        {
            if (horizontal == 2)
            {
                horizontal = -1;
            }
            GetComponent<CharController>().SetHorizontal(horizontal);
            if (vertical == 2)
            {
                vertical = -1;
            }
            GetComponent<CharController>().SetVertical(vertical);
            GetComponent<CharController>().SetAttackP(System.Convert.ToBoolean(attackP));
            GetComponent<CharController>().SetAttackK(System.Convert.ToBoolean(attackK));
            GetComponent<CharController>().SetAttackUB(System.Convert.ToBoolean(attackUB));
            GetComponent<CharController>().SetBlock(System.Convert.ToBoolean(block));
        }
        else
        {
            GetComponent<CharController>().SetHorizontal(0);
            GetComponent<CharController>().SetVertical(0);
            GetComponent<CharController>().SetAttackP(false);
            GetComponent<CharController>().SetAttackK(false);
            GetComponent<CharController>().SetAttackUB(false);
            GetComponent<CharController>().SetBlock(false);
        }

        float Reward = 0f;

        // If lost health: -Health loss
        // If dealt damage: +Damage dealt
        // If blocked an attack: +10
        // If won: +1
        // If lost: -1
        // Normalize reward between -1 and 1

        if (this.GetComponent<HealthScript>().GetHeath() < m_InitialHealth)
        {
            Reward -= m_InitialHealth - this.GetComponent<HealthScript>().GetHeath();
            m_InitialHealth = this.GetComponent<HealthScript>().GetHeath();
        }

        if (target.GetComponent<HealthScript>().GetHeath() < m_TargetInitialHealth)
        {
            Reward += m_TargetInitialHealth - target.GetComponent<HealthScript>().GetHeath();
            m_TargetInitialHealth = target.GetComponent<HealthScript>().GetHeath();
        }

        if (this.GetComponent<CharController>().GetHitRecieved() 
            && this.GetComponent<StateScript>().GetCurrentState() == StateScript.State.Blockstun)
        {
            Reward += 10;
        }

        if (m_HasWon == true)
        {
            Reward += 1f;
        }
        else if (m_HasLost == true)
        {
            Reward -= 1f;
        }

        Reward = 2 * ((Reward + 18f) / (18 + 18)) - 1;
        SetReward(Reward);

        Monitor.Log("Reward", Reward, null);
        Monitor.Log("Horizontal", horizontal, null);
        Monitor.Log("Vertical", vertical, null);
        Monitor.Log("AttackP", attackP, null);
        Monitor.Log("AttackK", attackK, null);
        Monitor.Log("AttackUB", attackUB, null);
        Monitor.Log("Block", block, null);

        if (m_HasWon || m_HasLost)
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[6];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        action[2] = System.Convert.ToSingle(Input.GetKeyDown("i"));
        action[3] = System.Convert.ToSingle(Input.GetKeyDown("o"));
        action[4] = System.Convert.ToSingle(Input.GetKeyDown("p"));
        action[5] = System.Convert.ToSingle(Input.GetKey("j"));

        return action;
    }

    public void SetHasWon(bool val)
    {
        m_HasWon = val;
    }

    public void SetHasLost(bool val)
    {
        m_HasLost = val;
    }
}
