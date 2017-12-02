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

        Vector3 moveDir = mainCamera.TransformDirection(horAxis, 0f, verAxis);

        Vector3 vel = rb.velocity;
        rb.velocity = moveDir * speed;
        rb.velocity = new Vector3(rb.velocity.x, vel.y, rb.velocity.z);
    }

    void Update()
    {
        if (GameController.gameState != GameController.GameState.Playing)
        {
            enabled = false;
        }
    }
}
