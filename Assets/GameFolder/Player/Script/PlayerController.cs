using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]private TrailRenderer tr;
    Rigidbody2D rb;
    Vector2 vel;
    float velocidade = 7f;
    float pulo = 950;

    public int comboNumber;
    public float comboTime;


    public Transform floorCollider;
    public Transform skin;
    public LayerMask floorLayer;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public bool canJump;


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

        if (isDashing)
            return;

        #region Vida 
        if (character.life <= 0)
            this.enabled = false;

        #endregion

        #region Dash

        if (Input.GetButtonDown("Fire2") && canDash)
        {
            animator.Play("PlayerDash");
            StartCoroutine(Dash());
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
        canJump = Physics2D.OverlapCircle(floorCollider.position, 0.1f, floorLayer);
        if (Input.GetButtonDown("Jump") && canJump && comboTime > 0.5f)
        {
            animator.Play("PlayerJump", -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, pulo));
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
        if (isDashing)
            return;

        rb.velocity = vel;
    }

    private IEnumerator Dash()
    {

        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(skin.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
