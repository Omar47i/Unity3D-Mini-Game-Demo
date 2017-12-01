using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    GameObject explosion;

    Rigidbody rb;

    int damage = 10;
   
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void InitializeBullet(Quaternion rot, Vector3 forward, float projSpeed, Vector3 playerPos, Vector3 playerVel)
    {
        Vector3 predictedPoint = predictedPosition(playerPos, transform.position, playerVel, projSpeed);

        Debug.Log(predictedPoint);
        rb.velocity = projSpeed * (predictedPoint - transform.position);
        Debug.DrawRay(transform.position, predictedPoint - transform.position, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // Inflict damage to the player
            other.gameObject.GetComponent<PlayerHP>().InflictDamage(damage);
        }

        Destroy(gameObject);
    }

    Vector3 predictedPosition(Vector3 targetPosition, Vector3 shooterPosition, Vector3 targetVelocity, float projectileSpeed)
    {
        Vector3 displacement = targetPosition - shooterPosition;
        float targetMoveAngle = Vector3.Angle(-displacement, targetVelocity) * Mathf.Deg2Rad;
        //if the target is stopping or if it is impossible for the projectile to catch up with the target (Sine Formula)
        if (targetVelocity.magnitude == 0 || targetVelocity.magnitude > projectileSpeed && Mathf.Sin(targetMoveAngle) / projectileSpeed > Mathf.Cos(targetMoveAngle) / targetVelocity.magnitude)
        {
            Debug.Log("Position prediction is not feasible.");
            return targetPosition;
        }
        //also Sine Formula
        float shootAngle = Mathf.Asin(Mathf.Sin(targetMoveAngle) * targetVelocity.magnitude / projectileSpeed);
        return targetPosition + targetVelocity * displacement.magnitude / Mathf.Sin(Mathf.PI - targetMoveAngle - shootAngle) * Mathf.Sin(shootAngle) / targetVelocity.magnitude;
    }


}
