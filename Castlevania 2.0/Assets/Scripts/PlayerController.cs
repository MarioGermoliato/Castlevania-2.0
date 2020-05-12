using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // acesso a outros scripts
    private CameraController _CameraController;
    private SoundManager _SoundManager;
    private UIManager _UIManager;

    public Rigidbody2D playerRB;
    
    public Animator playerAnimator;

    [Header("Movimentação")]
    [SerializeField]
    private float velocityPlayer;
    [SerializeField]
    private float jumpSpeed;    
    public bool stop;
    public float xJumping;
    public LayerMask floorLayer;

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

    [Header("Itens")]
    public bool[] itemUp;
    public GameObject dagger;


    [Header("ItensVel")]
    [SerializeField]
    private Vector2 daggerForce;
      
    [SerializeField]
    private int faseTime;

    public bool cantMove;



    // Start is called before the first frame update
    void Start()
    {
        _CameraController = FindObjectOfType(typeof(CameraController)) as CameraController;
        _SoundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;
        _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        _CameraController.playerTransform = this.transform;

        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerHitBox = GetComponent<BoxCollider2D>();

        StartCoroutine("TimeCounter");
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        Crouch();
        Attack();       
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowDagger();
        }
    }
    void FixedUpdate()
    {               
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, floorLayer);
    }    

    void MoveCharacter()
    {
        if (cantMove == false)
        {
            float x = Input.GetAxisRaw("Horizontal");
            


            if (isGrounded == true)
            {
                xJumping = x;
            }

            if (stop == true)
            {
                x = 0;
                xJumping = 0;
            }

            if (isAttacking == true && isGrounded == true)
            {
                x = 0;
                xJumping = 0;
            }

            float speedY = playerRB.velocity.y;
            playerRB.velocity = new Vector2(xJumping * velocityPlayer, speedY);
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

    }

    void Crouch()
    {
        if (cantMove == false)
        {
            if (Input.GetKey(KeyCode.S) && isGrounded == true)
            {
                isCrouched = true;
                playerRB.velocity = new Vector2(0, playerRB.velocity.y);
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                isCrouched = false;
            }
            playerAnimator.SetBool("isCrouched", isCrouched);
        }
    }

    void Attack()
    {
        if (cantMove == false)
        {
            if (Input.GetButtonDown("Fire1") /*&& isAttacking == false*/)
            {
                isAttacking = true;
                playerAnimator.SetTrigger("isAttacking");

            }
        }


    }  

    void EndAttack()
    {
        StartCoroutine("DelayAttack");
    }
    void EndStop()
    {
        stop = false;
        cantMove = false;
    }
    public void WeaponUp(int ItemId)
    {
        int i = 0;
        foreach (var item in itemUp)
        {            
            itemUp[i] = false;
            i++;
        }
        itemUp[ItemId] = true;
    }

    private void ToScore(int nPoints)
    {
        GlobalStats.score += nPoints;
        _UIManager.scoreTxt.text = "SCORE-" + GlobalStats.score.ToString();        
    }

    private void CollectHearts(int nHearts )
    {
        GlobalStats.hearts += nHearts;
        _UIManager.heartsTxt.text = "-" + GlobalStats.hearts.ToString();
    }    
    
    public void ThrowDagger()
    {
        GameObject Dagger = Instantiate(dagger, hand.position, hand.rotation);
        Dagger.GetComponent<Rigidbody2D>().AddForce(transform.TransformDirection(daggerForce), ForceMode2D.Impulse);
    }

    public void PlayAttackFx()
    {
        _SoundManager.audioSource.PlayOneShot(_SoundManager.attackWhip);
    }

    public void GetUpgrade()
    {
            GlobalStats.powerUps += 1;
            playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
            Debug.Log("Coletou1");
            playerAnimator.SetTrigger("powerUpCollect");
            _SoundManager.audioSource.PlayOneShot(_SoundManager.collectUpgrade);
            Debug.Log("Coletou2");
            stop = true;
            Debug.Log(GlobalStats.powerUps);        
    }

    public void GlobalUpgradeCounter()
    {
        
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.12f);
        isAttacking = false;
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1);
        faseTime -= 1;
        _UIManager.timeTxt.text = "TIME 0" + faseTime;
        StartCoroutine("TimeCounter");
    }
}
