using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1.0f;
    public float hitDamage = 1.0f;
    public float blockDamage = 1.0f;
    public GameObject character;

    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius, layer);

        if (collisions.Length > 0)
        {
            if (collisions[0].GetComponent<StateScript>().GetCurrentState() == StateScript.State.Block
                || collisions[0].GetComponent<StateScript>().GetCurrentState() == StateScript.State.Walk_Block
                || collisions[0].GetComponent<StateScript>().GetCurrentState() == StateScript.State.Blockstun)
            {
                collisions[0].GetComponent<HealthScript>().ApplyDamage(blockDamage);
                collisions[0].GetComponent<StateScript>().SetCurrentState(StateScript.State.Blockstun);
            }
            else
            {
                collisions[0].GetComponent<HealthScript>().ApplyDamage(hitDamage);
                collisions[0].GetComponent<StateScript>().SetCurrentState(StateScript.State.Hitstun);
            }
            gameObject.SetActive(false);
        }
    }
}
