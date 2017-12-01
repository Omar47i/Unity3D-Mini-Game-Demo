using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    GameObject projectile;

    Transform player;

    float attackFreq = 1f;
    float attackTimer = 0f;
    float rotationSpeed = 60f;
    float projectileSpeed = 30f;
    float coverageArea = 15f;

    Quaternion targetRotation;
    Transform firePosition;

    void Awake()
    {
        firePosition = transform.GetChild(0);

        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
    }

    void Update()
    {
        if (GameController.gameState == GameController.GameState.GameOver)
            return;

        FacePlayer();

        AttackPlayer();
    }

    void AttackPlayer()
    {
        // Attack player only if he is within coverage area
        if (isPlayerWithinRange())
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0)
            {
                attackTimer = attackFreq;

                GameObject bullet = Instantiate(projectile, firePosition.position, Quaternion.identity);

                bullet.GetComponent<Projectile>().InitializeBullet(transform.rotation, transform.forward, projectileSpeed, player.position, player.GetComponent<Rigidbody>().velocity);
            }
        }
        else
        {
            // When player get out of sight, reset attack timer
            attackTimer = attackFreq;
        }
    }

    public void InitializeTurret(float projSpeed, float coverageA)
    {
        projectileSpeed = projSpeed;
        coverageArea = coverageA;
    }

    void FacePlayer()
    {
        // Face the player only if he is within the coverage area
        if (isPlayerWithinRange())
        {
            // direction to the player
            Vector3 dirToPlayer = player.position - transform.position;

            targetRotation = Quaternion.LookRotation(dirToPlayer);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    bool isPlayerWithinRange()
    {
        return (Vector3.Distance(transform.position, player.position) <= coverageArea) ? true : false;
    }
}