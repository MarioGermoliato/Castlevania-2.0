using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItens : MonoBehaviour
{
    public enum ItensDrop {LittleHeart, BigHeart, Upgrade }

    public ItensDrop dropItem;

    [SerializeField]
    private GameObject powerUp;
    [SerializeField]
    private GameObject smallHeart;
    [SerializeField]
    private GameObject bigHeart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem()
    {
        if (dropItem == ItensDrop.Upgrade)
        {
            if (GlobalStats.powerUps < 2)
            {
                Instantiate(powerUp);
            }
            else
            {
                Instantiate(bigHeart);
            }
        }
        else if (dropItem == ItensDrop.BigHeart)
        {
            Instantiate(bigHeart);
        }
        else if (dropItem == ItensDrop.LittleHeart)
        {
            Instantiate(smallHeart);
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whip"))
        {
            DropItem();
        }
    }
}
