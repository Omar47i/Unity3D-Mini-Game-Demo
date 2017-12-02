using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Transform mainCamera;          // to move the player in the forward direction of the camera

    Rigidbody rb;
    float speed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main.transform;
    }

    void FixedUpdate()
    {
        float horAxis = Input.GetAxisRaw("Horizontal");
        float verAxis = Input.GetAxisRaw("Vertical");

        //Vector3 moveDir = transform.TransformDirection(horAxis, 0f, verAxis);
        //Vector3 moveDir = new Vector3(horAxis, 0f, verAxis);
        Vector3 moveDir = mainCamera.TransformDirection(horAxis, 0f, verAxis);

        rb.velocity = moveDir * speed;

        //if (horAxis == 0f && verAxis == 0f)
        //{
        //    rb.velocity = Vector3.zero;
        //}
    }

    void Update()
    {
        if (GameController.gameState != GameController.GameState.Playing)
        {
            enabled = false;
        }
    }
}
