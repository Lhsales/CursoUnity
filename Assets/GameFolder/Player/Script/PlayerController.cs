using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    float velocidade = 2;

    int numeroCombo;
    public float tempoCombo;

    public Transform floorCollider;
    public Transform skin;
    public LayerMask floorLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = new Vector2(5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var animator = skin.GetComponent<Animator>();
        var horizontal = Input.GetAxisRaw("Horizontal");

        tempoCombo = tempoCombo + Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && tempoCombo > 0.5f)
        {
            numeroCombo++;
            if (numeroCombo > 2)
                numeroCombo = 1;

            tempoCombo = 0;
            animator.Play("PlayerAttack" + numeroCombo, -1);

        }
        if (tempoCombo >= 1)
            numeroCombo = 0;


        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.1f, floorLayer);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            animator.Play("PlayerJump", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 150));
        }

        vel = new Vector2(horizontal * velocidade, rb.velocity.y);

        if (horizontal != 0)
        {
            skin.localScale = new Vector3(horizontal, 1, 1);
            animator.SetBool("PlayerRun", true);
        }
        else
            animator.SetBool("PlayerRun", false);
        
    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }
}
