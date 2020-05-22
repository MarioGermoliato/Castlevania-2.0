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

    private SpriteRenderer playerSR;
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
    public GameObject dagger;
    public bool isThrowing;


    [Header("ItensVel")]
    [SerializeField]
    private Vector2 daggerForce;   
      
    
    public bool cantMove;

    public bool dontWalkPlease;

    public bool damageUp;
    public bool isLookingLeft;

    [Header("Damage")]
    [SerializeField]
    private Color damageColor;
    [SerializeField]
    private Color noHitColor;

    [Header("Ladder")]
    public bool canMove = true;
    public bool upStairs;
    public bool downStairs;



    // Start is called before the first frame update
    void Start()
    {
        _CameraController = FindObjectOfType(typeof(CameraController)) as CameraController;
        _SoundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;
        _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        _CameraController.playerTransform = this.transform;

        playerRB = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
        playerHitBox = GetComponent<BoxCollider2D>();

        StartCoroutine("TimeCounter");

        Debug.Log("numero upgrades " + GlobalStats.powerUps);
        playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
    }

    // Update is called once per frame
    void Update()
    {       

        if (dontWalkPlease == false && canMove)
        {
            MoveCharacter();
        }
        Crouch();
        Attack();
        ThrowDagger();         
                       
    }
    void FixedUpdate()
    {               
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f, floorLayer);

        if (isGrounded == true)
        {
            damageUp = false;
        }

        if (onClimbable || isClimbing) //Check if the Player is on a Climbable or Climbing one
            UseClimbable();

    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeDamage(2);
            GetHurt();
            StartCoroutine("DamageControl");
            UpNumberOfHearts();            
        }
        else if (collision.gameObject.CompareTag("Transition1"))
        {
            StopAllCoroutines();
        }

    }
    // STAIRSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

    bool onClimbable = false;
    bool isClimbing = false;
    float climbPercentage;
    float ClimbingSpeed = 0.5f;
    Vector2 vectorStart, vectorEnd; //Starting and Ending Point of the Climbable
    void UseClimbable()
    {
        float inputVer = Input.GetAxisRaw("Vertical");

        if (inputVer != 0)
        {
            //Climb base on the percentage so we could back and forward based on the inputVer
            climbPercentage += Time.deltaTime * ClimbingSpeed * inputVer;
            this.gameObject.transform.position = Vector2.Lerp(vectorStart, vectorEnd, climbPercentage);
        }

        climbPercentage = Mathf.Clamp01(climbPercentage);

        //if the Player reaches any end he can move again
        if (climbPercentage == 0 || climbPercentage == 1)
        {
            isClimbing = false;
            canMove = true;
            playerRB.gravityScale = 2.5f;
            playerAnimator.SetBool("upStairs", isClimbing);
            playerAnimator.SetBool("downStairs", isClimbing);
        }
        else
        {
            isClimbing = true;
            canMove = false;
            playerRB.gravityScale = 0;
            playerRB.velocity = new Vector2(0, 0);
            playerAnimator.SetBool("upStairs", upStairs);
            playerAnimator.SetBool("downStairs", downStairs);
        }
    }

    //Called to set the Climbable Data
    public void SetClimbableData(bool onClimbable, Vector2 StartY, Vector2 EndY, bool isDown, float ClimbingSpeed)
    {
        this.onClimbable = onClimbable;

        this.vectorStart = StartY;
        this.vectorEnd = EndY;

        //to Check at what end the Player is
        if (isDown)
        {
            climbPercentage = 0;
            upStairs = true;
            downStairs = false;
        }
        else
        {
            climbPercentage = 1;
            upStairs = false;
            downStairs = true;
            
        }

        this.ClimbingSpeed = ClimbingSpeed;
    }
    public void OffClimbable()
    {
        onClimbable = false;
    }





    //STAIRSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS



    void TakeDamage(int Damage)
    {
        GlobalStats.playerLife -= Damage;
    }

    void GetHurt()
    {
        damageUp = true;
        if (isLookingLeft == false)
        {
            playerRB.AddForce(new Vector2(-2f, 8), ForceMode2D.Impulse);
        }
        else
        {
            playerRB.AddForce(new Vector2(2, 8), ForceMode2D.Impulse);
        }

     
    }

    void EndHurt()
    {
        damageUp = false;     
    }

    void MoveCharacter()
    {       
            if (cantMove == false)
            {

                float x = Input.GetAxisRaw("Horizontal");                

                if (isThrowing == true && isGrounded == true)
                {
                    x = 0;
                    xJumping = 0;
                }

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
            if (Input.GetAxisRaw("Horizontal") != 0 && damageUp == false)
            {
                playerRB.velocity = new Vector2(xJumping * velocityPlayer, speedY);
            }
                  
            if (damageUp == true)
            {
            playerRB.velocity = new Vector2(playerRB.velocity.x, speedY);
            }       
                            
                playerAnimator.SetFloat("MoveSpeed", Mathf.Abs(x));
                playerAnimator.SetBool("isGrounded", isGrounded);


                if (Input.GetButtonDown("Jump") && isGrounded == true)
                {
                    playerRB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                }

            if (x > 0.05f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                isLookingLeft = false;
            }
            else if (x < -0.05f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                isLookingLeft = true;
            }
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
        foreach (var item in _UIManager.itemUp)
        {
            _UIManager.itemUp[i] = false;
            i++;
        }
        _UIManager.itemUp[ItemId] = true;
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
        if (Input.GetKeyDown(KeyCode.Q) && cantMove == false && _UIManager.itemUp[0] && GlobalStats.hearts > 0 && isThrowing == false)
        {
            isThrowing = true;
            playerAnimator.SetTrigger("ThrowingItem");
            
            GlobalStats.hearts--;
            _UIManager.heartsTxt.text = "-" + GlobalStats.hearts.ToString();

            StartCoroutine("DelayItem");
        }
       
    }

    public void SetThrowAnimation()
    {
        _SoundManager.audioSource.PlayOneShot(_SoundManager.throwDaager);
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

    public void UpNumberOfHearts()
    {
       for (int i = 0; i < _UIManager.playerLifesIcons.Length; i++)
        {
            if (i < GlobalStats.playerLife)
            {
                _UIManager.playerLifesIcons[i].sprite = _UIManager.fullLifePlayer;
            }
            else
            {
                _UIManager.playerLifesIcons[i].sprite = _UIManager.emptyLife;
            }
        }
    }
    
    public void DontWalkBegin()
    {
        dontWalkPlease = true;
    }

    public void DontWalkEnd()
    {
        dontWalkPlease = false;
    }

    IEnumerator DelayItem()
    {
        yield return new WaitForSeconds(0.5f);
        isThrowing = false;            
    }

    IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.12f);
        isAttacking = false;
    }

    IEnumerator TimeCounter()
    {
        yield return new WaitForSeconds(1);
        GlobalStats.faseTime -= 1;
        _UIManager.timeTxt.text = "TIME 0" + GlobalStats.faseTime;
        StartCoroutine("TimeCounter");
    }

    IEnumerator DamageControl()
    {                
        this.gameObject.layer = LayerMask.NameToLayer("Invencivel");
        playerSR.color = damageColor;
        yield return new WaitForSeconds(0.3f);
        playerSR.color = noHitColor;
        for (int i = 0; i < 5; i++)
        {
            playerSR.enabled = false;
            yield return new WaitForSeconds(0.2f);
            playerSR.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        playerSR.color = Color.white;
    }
}
