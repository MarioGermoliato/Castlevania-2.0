using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnemy : MonoBehaviour
{
    [Header("Locations of respawl")]
    [SerializeField]
    private Transform[] respawlLocations;

    [SerializeField]
    private float timeToRespawl;

    public Enemy[] enemyPrefab;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopCoroutine("RespawlDelay");
            RespawlEnemy();
            StartCoroutine("RespawlDelay");
        }

    }



    public void RespawlEnemy()
    {
        int i = Random.Range(0, respawlLocations.Length);
        int b = Random.Range(0, enemyPrefab.Length);
        int c = Random.Range(0, 2);

        Enemy currentEnemy = Instantiate(enemyPrefab[b], new Vector3(respawlLocations[i].position.x, respawlLocations[i].position.y, respawlLocations[i].position.z), respawlLocations[i].rotation);
        Debug.Log(c);

        if (c == 0)
        {
            Enemy currentEnemy2 = Instantiate(enemyPrefab[b], new Vector3(respawlLocations[i].position.x + 1.5f, respawlLocations[i].position.y, respawlLocations[i].position.z), respawlLocations[i].rotation);
            Enemy currentEnemy3 = Instantiate(enemyPrefab[b], new Vector3(respawlLocations[i].position.x + 3f, respawlLocations[i].position.y, respawlLocations[i].position.z), respawlLocations[i].rotation); if (i % 2 == 0)
                if (i % 2 == 0)
                {
                    currentEnemy2.direction = 1;
                    currentEnemy3.direction = 1;
                }
                else
                {
                    currentEnemy2.direction = -1;
                    currentEnemy3.direction = -1;
                }
        }

        if (i % 2 == 0)
        {
            currentEnemy.direction = 1;
        }
        else
        {
            currentEnemy.direction = -1;
        }

    }

    IEnumerator RespawlDelay()
    {
        yield return new WaitForSeconds(timeToRespawl);
        RespawlEnemy();
        StartCoroutine("RespawlDelay");

    }

}