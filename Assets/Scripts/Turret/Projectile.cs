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

        Destroy(gameObject, 4f);
    }

    public void InitializeBullet(Vector3 shooterPos, float projSpeed, Vector3 playerPos, Vector3 playerVel /*Quaternion rot, Vector3 forward*/)
    {
        Vector3 predictedPoint = FirstOrderIntercept(shooterPos, Vector3.zero, projSpeed, playerPos, playerVel);

        rb.AddRelativeForce(projSpeed * (predictedPoint - transform.position).normalized, ForceMode.VelocityChange);
        //rb.velocity = projSpeed * (predictedPoint - transform.position).normalized;

        Debug.DrawRay(transform.position, predictedPoint - transform.position, Color.red);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // Inflict damage to the player
            other.gameObject.GetComponent<PlayerHP>().InflictDamage(damage);

            GameObject exp = Instantiate(explosion, other.transform.position, Quaternion.identity);

            Destroy(exp, 1.25f);
        }

        Destroy(gameObject);
    }

    //first-order intercept using absolute target position
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    )
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        Debug.Log("Time to reach: " + t);
        //if (t > 9f)
        //{
        //    Debug.LogError("Projectile Speed: " + shotSpeed + "\nTarget Relative Position: " + targetRelativePosition +
        //        "\nTarget Relative Velocity: " + targetRelativeVelocity + "\nTime: " + t);
        //    Debug.Break();
        //}
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /(2f * Vector3.Dot(targetRelativeVelocity,
                targetRelativePosition));
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }

}
