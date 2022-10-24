using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 vel;
    float velocidade = 2;

    int comboNumber;
    public float comboTime;

    public float dashTime;

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
        var character = GetComponent<Character>();

        #region Vida 
        if (character.life <= 0)
            this.enabled = false;

        #endregion

        #region Dash
        dashTime += Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && dashTime > 0.5f)
        {
            dashTime = 0f;
            animator.Play("PlayerDash", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(150 * skin.localScale.x, 0));
        }

        #endregion

        #region Ataque
        comboTime = comboTime + Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && comboTime > 0.5f)
        {
            comboNumber++;
            if (comboNumber > 2)
                comboNumber = 1;

            comboTime = 0;
            animator.Play("PlayerAttack" + comboNumber, -1);

        }
        if (comboTime >= 1)
            comboNumber = 0;

        #endregion

        #region Pulo
        bool canJump = Physics2D.OverlapCircle(floorCollider.position, 0.1f, floorLayer);
        if (Input.GetButtonDown("Jump") && canJump)
        {
            animator.Play("PlayerJump", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, 150));
        }

        #endregion

        #region Movimentação

        vel = new Vector2(horizontal * velocidade, rb.velocity.y);

        if (horizontal != 0)
        {
            skin.localScale = new Vector3(horizontal, 1, 1);
            animator.SetBool("PlayerRun", true);
        }
        else
            animator.SetBool("PlayerRun", false);
        #endregion
    }

    private void FixedUpdate()
    {
        if (dashTime > 0.5f)
            rb.velocity = vel;
    }
}
