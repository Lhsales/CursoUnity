using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
    public Transform player;
    public Transform skin;
    public float attackTime;

    float velocidade = 2f;

    // Start is called before the first frame update
    void Start()
    {
        attackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");

        #region Morte
        if (GetComponent<Character>().life <= 0)
        {

            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;

            this.enabled = false;
        }
        #endregion

        #region Movimentação e Dano

        if (Vector2.Distance(transform.position, player.GetComponent<CapsuleCollider2D>().bounds.center) > 0.9f)
        {
            attackTime = 0;
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<CapsuleCollider2D>().bounds.center, velocidade * Time.deltaTime);
            if (horizontal != 0)
                skin.localScale = new Vector3(horizontal, 1, 1);
        }
        else
        {
            attackTime += Time.deltaTime;
            if (attackTime >= 0.5f)
            {
                attackTime = 0;
                player.GetComponent<Character>().PlayerDamage(1);
            }
        }

        #endregion
    }
}
