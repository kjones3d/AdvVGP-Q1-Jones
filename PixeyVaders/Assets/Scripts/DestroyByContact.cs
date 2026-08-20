using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public PlayerShooting player;

    void Awake()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            player = playerObject.GetComponent<PlayerShooting>();
    }

    void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.CompareTag("PlayerShot") )
        {
            player.nextFire = 0;
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if ( other.gameObject.CompareTag("Player") )
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
            return;
    }
}