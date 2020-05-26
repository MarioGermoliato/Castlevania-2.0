using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    private UIManager _UIManager;
    private SoundManager _SoundManager;

    [SerializeField]
    private float velMov;
       

    [SerializeField]
    private Rigidbody2D zombieRB;

   

    // Start is called before the first frame update
    void Start()
    {
        zombieRB = GetComponent<Rigidbody2D>();
        health = 1;
        _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;
        _SoundManager = FindObjectOfType(typeof(SoundManager)) as SoundManager;
        enemyAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        ZombieWalk();
        Defeat(100);
    }

    public void ZombieWalk()
    {
        zombieRB.velocity = new Vector2(velMov * direction, zombieRB.velocity.y);
    }
    public override void Defeat(int points)
    {
        

        if (health <= 0)
        {
            GlobalStats.score += points;
            _UIManager.scoreTxt.text = "SCORE-00" + GlobalStats.score.ToString();
            velMov = 0;
            enemyAnimator.SetTrigger("enemyDeath");
            
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Whip")) 
        {
            GetDamage(2);
            _SoundManager.audioSource.PlayOneShot(_SoundManager.destroyTorch);

        }
        else if (collision.CompareTag("Dagger"))
        {
            GetDamage(2);
            _SoundManager.audioSource.PlayOneShot(_SoundManager.destroyTorch);
            Destroy(collision.gameObject);
        }
        else if(collision.CompareTag("End"))
        {
            Destroy(this.gameObject);
        }
    }    
   

}
