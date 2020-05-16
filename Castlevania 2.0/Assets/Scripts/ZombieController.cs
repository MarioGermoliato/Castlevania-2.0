﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : Enemy
{
    private UIManager _UIManager;

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
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Whip")) 
        {
            GetDamage(2);
        }
        else if (collision.CompareTag("Weapon"))
        {
            GetDamage(2);
            Destroy(collision.gameObject);
        }
    }    


}
