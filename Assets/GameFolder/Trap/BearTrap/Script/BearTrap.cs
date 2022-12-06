using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    public Transform skin;

    Transform player;
    Transform playerSkin;

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
            GetComponent<BoxCollider2D>().enabled = false;
            skin.GetComponent<Animator>().Play("Stuck", -1);

            player = collision.transform;
            player.GetComponent<PlayerController>().GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            playerSkin = player.GetComponent<PlayerController>().skin;
            playerSkin.GetComponent<Animator>().SetBool("PlayerRun", false);
            playerSkin.GetComponent<Animator>().Play("PlayerIdle", -1);


            collision.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Character>().PlayerDamage(1);
            Invoke("ReleasePlayer", 2);
        }
    }

    void ReleasePlayer()
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
}
