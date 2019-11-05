using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject m_Character;
    private float yaw = 0.0f;
    public float sensitivity = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_Character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += sensitivity * Input.GetAxisRaw("Mouse X");
        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        m_Character.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}