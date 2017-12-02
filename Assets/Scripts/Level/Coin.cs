using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    GameObject sparklesVFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.Player)
        {
            // Add coins to the player
            GameController.Instance.AddCoins(1);

            // Create sparkles effect
            GameObject vfx = Instantiate(sparklesVFX, other.transform.position, Quaternion.identity);

            Destroy(vfx, 1.25f);
        }

        Destroy(gameObject);
    }
}
