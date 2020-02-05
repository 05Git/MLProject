using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
     // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool attackP = Input.GetKeyDown("i");
        bool attackK = Input.GetKey("o");
        bool attackUB = Input.GetKey("p");
        bool block = Input.GetKey("j");
        GetComponent<CharController>().SetHorizontal(horizontal);
        GetComponent<CharController>().SetVertical(vertical);
        GetComponent<CharController>().SetAttackP(attackP);
        GetComponent<CharController>().SetAttackK(attackK);
        GetComponent<CharController>().SetAttackUB(attackUB);
        GetComponent<CharController>().SetBlock(block);
    }
}
