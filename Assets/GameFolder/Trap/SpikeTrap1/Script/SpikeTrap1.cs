using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap1 : MonoBehaviour
{
    public float repulsao;

    Rigidbody2D rbPlayer;

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
            rbPlayer = collision.transform.GetComponent<Rigidbody2D>();
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(new Vector2(0, repulsao));

            collision.transform.GetComponent<Character>().life--;
        }
    }
}
