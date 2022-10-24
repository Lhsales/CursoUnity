using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public Transform player;

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
        if (collision.CompareTag("Enemy"))
        {
            var character = collision.GetComponent<Character>();
            var playerController = player.GetComponent<PlayerController>();

            if (playerController.comboNumber == 1)
                character.life--;
            else if (playerController.comboNumber == 2)
                character.life -= 2;

        }
    }
}
