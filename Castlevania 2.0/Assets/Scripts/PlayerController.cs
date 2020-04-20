using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D playerRB;
    
    public Animator playerAnimator;

    [Header("Movimentação")]
    [SerializeField]
    private float velocityPlayer;
    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private BoxCollider2D playerHitBox;
    [SerializeField]
    private bool isCrouched;

    [SerializeField]
    private Transform groundCheck;
    private bool isGrounded;

    [Header("Attack")]
    [SerializeField]
    private bool isAttacking;
    [SerializeField]
    private Transform hand;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Crouch();
        Attack();
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    void MoveCharacter()
    {
        float x = Input.GetAxis("Horizontal");
        float speedY = playerRB.velocity.y;
        playerRB.velocity = new Vector2(x * velocityPlayer, speedY);
        playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(x));
        playerAnimator.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {            
            playerRB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        }

        if (x > 0.05f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (x < -0.05f)
            transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    void Crouch()
    {
        if (Input.GetKey(KeyCode.S))
        {
            isCrouched = true;
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            isCrouched = false;
        }
        playerAnimator.SetBool("isCrouched", isCrouched);
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1") /*&& isAttacking == false*/)
        {
            //isAttacking = true;
            playerAnimator.SetTrigger("isAttacking");
            playerRB.velocity = new Vector2(0, playerRB.velocity.y);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUp"))
        {
            GlobalStats.powerUps += 1;
            playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
            Destroy(collision.gameObject);
        }
    }
}
