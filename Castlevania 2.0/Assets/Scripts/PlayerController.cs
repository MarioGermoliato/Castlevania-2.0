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
      
    [SerializeField]
    private int faseTime;

    public bool upgradeCatch;



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
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.02f);
    }

    void MoveCharacter()
    {        
            float x = Input.GetAxis("Horizontal");   
        if (stop == true)
        {
            x = 0;
        }

        if (isAttacking == true && isGrounded == true)
        {
            x = 0;
        }

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
        if (Input.GetKey(KeyCode.S) && isGrounded == true)
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
            isAttacking = true;
            playerAnimator.SetTrigger("isAttacking");
           
        }


    }

    void EndAttack()
    {
        StartCoroutine("DelayAttack");
    }
    void EndStop()
    {
        stop = false;
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

   /* private void OnTriggerEnter2D(Collider2D collision)
    {   
        
            if (collision.CompareTag("PowerUp"))
            {
                GlobalStats.powerUps += 1;
                playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
                Debug.Log("Coletou1");
                playerAnimator.SetTrigger("powerUpCollect");
                _SoundManager.audioSource.PlayOneShot(_SoundManager.collectUpgrade);
                Debug.Log("Coletou2");
                stop = true;
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("SmallHeart"))
            {
                CollectHearts(1);
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("BigHeart"))
            {
                CollectHearts(5);
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("RedBag"))
            {
                ToScore(100);
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("PurpleBag"))
            {
                ToScore(300);
                Destroy(collision.gameObject);
            }
            else if (collision.CompareTag("WhiteBag"))
            {
                ToScore(700);
                Destroy(collision.gameObject);
            }
        
    }*/

    public void PlayAttackFx()
    {
        _SoundManager.audioSource.PlayOneShot(_SoundManager.attackWhip);
    }

    public void GetUpgrade()
    {      
            upgradeCatch = false;
            GlobalStats.powerUps += 1;
            playerAnimator.SetInteger("powerUp", GlobalStats.powerUps);
            Debug.Log("Coletou1");
            playerAnimator.SetTrigger("powerUpCollect");
            _SoundManager.audioSource.PlayOneShot(_SoundManager.collectUpgrade);
            Debug.Log("Coletou2");
            stop = true;
            Debug.Log(GlobalStats.powerUps);
        
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
