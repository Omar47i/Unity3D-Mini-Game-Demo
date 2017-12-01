using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Rigidbody rb;
    float speed = 10f;

    float horAxis;
    float verAxis;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horAxis = Input.GetAxis("Horizontal");
        float verAxis = Input.GetAxis("Vertical");
        
        Vector3 moveDir = transform.TransformDirection(horAxis, 0f, verAxis);

        rb.AddForce(moveDir * 70f);
    }
}
