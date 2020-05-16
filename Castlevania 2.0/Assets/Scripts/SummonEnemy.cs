using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [Header("Locations of respawl")]
    [SerializeField]
    private Transform[] respawlLocations;
    private enum typeMonster { Zombie, Bat}

    private typeMonster CurrentMonster;

    [SerializeField]
    private bool playerOnTheTrigger;

    [SerializeField]
    private GameObject zombiePrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnTheTrigger = true;
            Debug.Log("MonsterHunter");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnTheTrigger = false;
            Debug.Log("DebocharLegal");
        }
       
        
    }

    public void RespawlEnemy()
    {
        if (playerOnTheTrigger == true)
        {

        }
    }

    
}
