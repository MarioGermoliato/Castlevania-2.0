using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;    
    protected UIManager _UIManager;    
    

    // Start is called before the first frame update
    void Start()
    {
        _UIManager = FindObjectOfType(typeof(UIManager)) as UIManager;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void GetDamage(int damage)
    {
        health -= damage;
    }

    public void Defeat(int points)
    {
        if (health <= 0)
        {
            ToScore(points);
            Destroy(this.gameObject);
        }
    }
    public void ToScore(int nPoints)
    {
        GlobalStats.score += nPoints;
        _UIManager.scoreTxt.text = "SCORE-" + GlobalStats.score.ToString();
    }
}
