﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    public int direction;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void GetDamage(int damage)
    {
        health -= damage;        
    }

    public virtual void Defeat(int points)
    {
        if (health <= 0)
        {            
            Destroy(this.gameObject);
        }
    }
    public void ToScore(int nPoints)
    {      

         
    }

    public void DirectionSet(int thisDirection)
    {
        direction = thisDirection;
    }
}
