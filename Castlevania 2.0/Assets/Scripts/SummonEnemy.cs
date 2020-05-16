using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [Header("Locations of respawl")]
    [SerializeField]
    private Transform[] respawlLocations;          
    [SerializeField]
    private bool playerOnTheTrigger;

    [SerializeField]
    private float timeToRespawl;
    
    public Enemy[] enemyPrefab;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnTheTrigger = true;
            StartCoroutine("RespawlDelay");            
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerOnTheTrigger = false;            
        }
       
        
    }

    public void RespawlEnemy()
    {
        if (playerOnTheTrigger == true)
        {
            int i = Random.Range(0, respawlLocations.Length);
            int b = Random.Range(0, enemyPrefab.Length);

           Enemy currentEnemy = Instantiate(enemyPrefab[b], new Vector3(respawlLocations[i].position.x, respawlLocations[i].position.y, respawlLocations[i].position.z), respawlLocations[i].rotation);

            if(i % 2 == 0)
            {
                currentEnemy.direction = 1;
            }
            else
            {
                currentEnemy.direction = -1;
            }
            
            
        }
    }

    IEnumerator RespawlDelay()
    {
        yield return new WaitForSeconds(timeToRespawl);
        RespawlEnemy();

        if(playerOnTheTrigger == true)
        {
            StartCoroutine("RespawlDelay");
        }
    }
    
}
