﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockableDamageScript : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1.0f;
    public float hitDamage = 1.0f;

    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius, layer);

        if (collisions.Length > 0)
        {
            collisions[0].GetComponent<HealthScript>().ApplyDamage(hitDamage);
            collisions[0].GetComponent<StateScript>().SetCurrentState(StateScript.State.Hitstun);
            gameObject.SetActive(false);
        }
    }
}