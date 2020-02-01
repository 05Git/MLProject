using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health = 100.0f;
    [SerializeField]
    private Image m_Health_UI;

    public float GetHeath()
    {
        return health;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (m_Health_UI != null)
        {
            m_Health_UI.fillAmount = health / 100.0f;
        }
        if (health <= 0.0f)
        {
            GetComponent<StateScript>().SetState(StateScript.State.Lose);
        }
    }
}
