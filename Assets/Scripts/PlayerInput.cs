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
        bool attack = Input.GetKeyDown("i");
        bool block = Input.GetKey("o");
        GetComponent<CharController>().SetHorizontal(horizontal);
        GetComponent<CharController>().SetVertical(vertical);
        GetComponent<CharController>().SetAttack(attack);
        GetComponent<CharController>().SetBlock(block);
    }
}
