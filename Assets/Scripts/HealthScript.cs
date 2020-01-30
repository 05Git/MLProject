using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public float health = 100.0f;

    public float GetHeath()
    {
        return health;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            GetComponent<StateScript>().SetState(StateScript.State.Lose);
        }
        else
        {
           GetComponent<StateScript>().SetState(StateScript.State.Hitstun);
        }
    }
}
