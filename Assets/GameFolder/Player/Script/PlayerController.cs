using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;

    public Transform floorCollider;
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

        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.1f, floorLayer);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 150));
        }
        vel = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);

    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }
}
