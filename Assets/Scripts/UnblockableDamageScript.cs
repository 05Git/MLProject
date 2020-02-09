using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockableDamageScript : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1.0f;
    public float hitDamage = 1.0f;
    public GameObject character;

    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius, layer);

        if (collisions.Length > 0)
        {
            if (collisions[0].GetComponent<StateScript>().GetCurrentState() != StateScript.State.Win
                || collisions[0].GetComponent<StateScript>().GetCurrentState() != StateScript.State.Lose)
            {
                collisions[0].GetComponent<HealthScript>().ApplyDamage(hitDamage);
                collisions[0].GetComponent<StateScript>().SetCurrentState(StateScript.State.Hitstun);
                collisions[0].GetComponent<CharController>().SetHitRecieved(true);
            }
            gameObject.SetActive(false);
        }
    }
}
