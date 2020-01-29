using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public LayerMask layer;
    public float radius = 1.0f;
    public float damage = 1.0f;

    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, radius, layer);

        if (collisions.Length > 0)
        {
            collisions[0].GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
