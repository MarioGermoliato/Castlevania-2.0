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
    public GameObject dagger;
    public bool isThrowing;


    [Header("ItensVel")]
    [SerializeField]
    private Vector2 daggerForce;

    [Header("Life")]
    public int playerLife;
      
    
    public bool cantMove;

    public bool dontWalkPlease;



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

        Debug.Log("numero upgrades " + GlobalStats.powerUps);
        playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
    }

    // Update is called once per frame
    void Update()
    {
        if (dontWalkPlease == false)
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            TakeDamage(2);
            UpNumberOfHearts();
            Destroy(collision.gameObject);
        }
    }
   

    void TakeDamage(int Damage)
    {
        playerLife -= Damage;
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
           // Debug.Log("Coletou1");
            playerAnimator.SetTrigger("powerUpCollect");
            _SoundManager.audioSource.PlayOneShot(_SoundManager.collectUpgrade);
           // Debug.Log("Coletou2");
            stop = true;
            Debug.Log(GlobalStats.powerUps);        
    }

    public void UpNumberOfHearts()
    {
       for (int i = 0; i < _UIManager.playerLifesIcons.Length; i++)
        {
            if (i < playerLife)
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
}
