using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    Rigidbody2D rbPlayer;

    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;

            rbPlayer = collision.transform.GetComponent<Rigidbody2D>();

            player.GetComponent<Character>().PlayerDamage(1);
        }
    }
}
